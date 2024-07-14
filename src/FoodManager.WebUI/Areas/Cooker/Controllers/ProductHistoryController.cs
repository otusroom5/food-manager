using FoodManager.WebUI.Areas.Cooker.Contracts;
using FoodManager.WebUI.Areas.Cooker.Models;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodManager.WebUI.Areas.Cooker.Controllers;

public sealed partial class CookerController : Abstractions.ControllerBase
{
    private readonly static string GetActionTypesListApiUrl = "/api/ProductHistory/GetActionTypesList";
    private readonly static string GetProductsByActionTypeInDateIntervalApiUrl = "/api/ProductHistory/GetProductsByActionTypeInDateInterval";
    private readonly static string GetActionsWithProductByDateApiUrl = "/api/ProductHistory/GetActionsWithProductByDate";

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> ProductHistory(string productId, string productName, string productAction, DateTime? dateFrom, DateTime? dateTo)
    {
        ProductHistoryIndexModel viewModel = new();

        HttpResponseMessage responseMessage;
        try
        {
            if (productId is null)
            {
                // Получаем типы действий с продуктом
                responseMessage = await _httpClient.GetAsync(GetActionTypesListApiUrl);

                string[] actionsResponse = await responseMessage.Content.ReadFromJsonAsync<string[]>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
                }

                viewModel.Actions = actionsResponse;

                // Получаем историю для всех продуктов
                dateFrom = dateFrom ?? DateTime.MinValue;
                dateTo = dateTo ?? DateTime.Now;

                var query = new List<KeyValuePair<string, string>>() 
                            { 
                                new ("actionType", productAction),
                                new ("dateStart", dateFrom.ToString()),
                                new ("dateEnd", dateTo.ToString())
                            };

                Uri requestUri = new UriBuilder(_httpClient.BaseAddress)
                {
                    Path = GetProductsByActionTypeInDateIntervalApiUrl,
                    Query = QueryString.Create(query).Value
                }.Uri;

                responseMessage = await _httpClient.GetAsync(requestUri);

                List<ProductHistory> productResponse = await responseMessage.Content.ReadFromJsonAsync<List<ProductHistory>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
                }

                viewModel.ProductHistoryItems = productResponse?.Select(f => f.ToModel()).OrderByDescending(p => p.CreatedAt).ToArray();
            }
            else
            {
                ProductModel product = new() { Id = productId, Name = productName };
                viewModel.Product = product;

                // Получаем историю для конкретного продукта
                var query = dateFrom is null 
                    ? new List<KeyValuePair<string, string>>() { new("productId", productId) } 
                    : new List<KeyValuePair<string, string>>()
                        {
                            new ("productId", productId),
                            new ("date", dateFrom.ToString())
                        };

                Uri requestUri = new UriBuilder(_httpClient.BaseAddress)
                {
                    Path = GetActionsWithProductByDateApiUrl,
                    Query = QueryString.Create(query).Value
                }.Uri;

                responseMessage = await _httpClient.GetAsync(requestUri);

                List<ProductHistory> productResponse = await responseMessage.Content.ReadFromJsonAsync<List<ProductHistory>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
                }

                viewModel.ProductHistoryItems = productResponse?.Select(f => f.ToModel()).ToArray();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return View(viewModel);
    }
}
