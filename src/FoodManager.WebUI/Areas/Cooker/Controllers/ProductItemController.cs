using FoodManager.WebUI.Areas.Cooker.Contracts;
using FoodManager.WebUI.Areas.Cooker.Models;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodManager.WebUI.Areas.Cooker.Controllers;

public sealed partial class CookerController : Abstractions.ControllerBase
{
    private readonly static string CreateProductItemApiUrl = "/api/ProductItem/Create";
    private readonly static string GetProductItemsApiUrl = "/api/ProductItem/GetAll";
    private readonly static string GetProductItemsByProductIdApiUrl = "/api/ProductItem/GetByProductId";
    private readonly static string GetUnitsApiUrl = "/api/Unit/GetByUnitType/";
    private readonly static string TakePartOfApiUrl = "/api/ProductItem/TakePartOf";
    private readonly static string WriteOffApiUrl = "/api/ProductItem/WriteOff";
    private readonly static string DeleteProductItemApiUrl = "/api/ProductItem/Delete/";

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> ProductItem(string productId, string productName, string unitType, string productAction)
    {
        ProductItemIndexModel viewModel = new();

        try
        {
            if (!Enum.TryParse<ProductAction>(productAction, true, out var action) || productId is null)
            {
                action = ProductAction.All;
            }
            viewModel.ProductAction = action;

            HttpResponseMessage responseMessage;
            List<ProductItem> itemsResponse;

            // Параметр передается если на страницу переходим для создания
            if (action is not ProductAction.All)
            {
                ProductModel product = new() { Id = productId, Name = productName, UnitType = unitType };
                viewModel.Product = product;

                // Получаем единицы измерения по типу
                string request = GetUnitsApiUrl + product.UnitType;

                responseMessage = await _httpClient.GetAsync(request);

                List<Unit> unitsResponse = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new HttpRequestException(message);
                }

                viewModel.Units = unitsResponse.Select(u => new UnitModel() { Id = u.Id, Name = u.Name }).ToArray();

                // Получаем единицы конкретного продукта в холодильнике
                string mainUnit = unitsResponse.Find(u => u.Coefficient == 1)?.Id;
                request = string.Join("/", GetProductItemsByProductIdApiUrl, product.Id, mainUnit);
                responseMessage = await _httpClient.GetAsync(request);

                itemsResponse = await responseMessage.Content.ReadFromJsonAsync<List<ProductItem>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new HttpRequestException(message);
                }
            }
            else
            {
                // Получаем все единицы продукта в холодильнике
                responseMessage = await _httpClient.GetAsync(GetProductItemsApiUrl);

                itemsResponse = await responseMessage.Content.ReadFromJsonAsync<List<ProductItem>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new HttpRequestException(message);
                }
            }

            viewModel.ProductItems = itemsResponse?.Select(i => i.ToModel()).OrderBy(i => i.ProductName).ThenBy(i => i.ExpiryDate).ToArray();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return View(viewModel);
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> CreateProductItem(ProductItemCreateModel model)
    {
        try
        {
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(CreateProductItemApiUrl, JsonContent.Create(model));

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product {model.ProductName} added to refrigerator";
            }
            else
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("ProductItem", new
        {
            productId = model.ProductId,
            productName = model.ProductName,
            unitType = model.UnitType,
            productAction = ProductAction.View.ToString()
        });
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> TakePartOfProduct(ProductTakeOfModel model)
    {
        try
        {
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(TakePartOfApiUrl, JsonContent.Create(model));

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product {model.ProductName} taken from refrigerator";
            }
            else
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("ProductItem", new
        {
            productId = model.ProductId,
            productName = model.ProductName,
            unitType = model.UnitType,
            productAction = ProductAction.View.ToString()
        });
    }


    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> WriteOffProductItem(string productId, string productName, string unitType, string productItemId)
    {
        try
        {
            if (!Guid.TryParse(productItemId, out Guid id))
            {
                throw new ArgumentException("Id is not correct");
            }

            List<Guid> model = new() { id };
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(WriteOffApiUrl, JsonContent.Create(model));

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product {productName} written off";
            }
            else
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("ProductItem", new
        {
            productId = productId,
            productName = productName,
            unitType = unitType,
            productAction = ProductAction.View.ToString()
        });
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> DeleteProductItem(string productId, string productName, string unitType, string productItemId)
    {
        try
        {
            if (!Guid.TryParse(productItemId, out _))
            {
                throw new ArgumentException("Id is not correct");
            }

            string request = DeleteProductItemApiUrl + productItemId.ToString();

            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product item of {productName} is deleted";
            }
            else
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("ProductItem", new
        {
            productId = productId,
            productName = productName,
            unitType = unitType,
            productAction = ProductAction.View.ToString()
        });
    }
}
