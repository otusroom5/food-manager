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
            // Получаем типы действий с продуктом
            responseMessage = await _httpClient.GetAsync(GetActionTypesListApiUrl);

            string[] actionsResponse = await responseMessage.Content.ReadFromJsonAsync<string[]>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }

            viewModel.Actions = actionsResponse;
            string request;

            if (productId is null)
            {
                // Получаем историю для конкретного продукта
                request = string.Join("/", GetProductsByActionTypeInDateIntervalApiUrl, productAction, dateFrom, dateTo);
                responseMessage = await _httpClient.GetAsync(request);

                List<ProductHistory> productResponse = await responseMessage.Content.ReadFromJsonAsync<List<ProductHistory>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
                }

                viewModel.ProductHistoryItems = productResponse?.Select(f => f.ToModel()).ToArray();
            }
            else
            {
                // Получаем историю для всех продуктов
                request = string.Join("/", GetActionsWithProductByDateApiUrl, productId, dateFrom);
                responseMessage = await _httpClient.GetAsync(request);

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
