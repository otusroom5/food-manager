using FoodManager.WebUI.Areas.Cooker.Contracts;
using FoodManager.WebUI.Areas.Cooker.Models;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodManager.WebUI.Areas.Cooker.Controllers;

[Area("Cooker")]
[Authorize(Roles = "Cooker")]
public sealed partial class CookerController : Abstractions.ControllerBase
{
    private readonly HttpClient _httpClient;
    private static readonly string GetProductsApiUrl = "/api/Product/GetAll";
    private static readonly string CreateProductApiUrl = "/api/Product/Create";
    private static readonly string DeleteProductApiUrl = "/api/Product/Delete/";
    private static readonly string GetUnitTypesApiUrl = "/api/Unit/GetAllUnitTypes";

    public CookerController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _httpClient = CreateStorageServiceClient();
    }

    [Route("{area}")]
    [Route("{area}/{controller}")]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Index()
    {
        ProductIndexModel viewModel = new();

        try
        {
            // Получаем список продуктов
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(GetProductsApiUrl);

            List<Product> productResponse = await responseMessage.Content.ReadFromJsonAsync<List<Product>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }

            viewModel.Products = productResponse?.Select(f => f.ToModel()).ToArray();

            // Получаем типы единиц измерения
            responseMessage = await _httpClient.GetAsync(GetUnitTypesApiUrl);

            string[] unitTypeResponse = await responseMessage.Content.ReadFromJsonAsync<string[]>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
            }

            viewModel.UnitTypes = unitTypeResponse;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return View(viewModel);
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> CreateProduct(ProductCreateModel model)
    {
        try
        {
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(CreateProductApiUrl, JsonContent.Create(model));

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product '{model.Name}' is created";
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

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        try
        {
            if (!Guid.TryParse(productId, out _))
            {
                throw new ArgumentException("Id is not correct");
            }

            string request = DeleteProductApiUrl + productId.ToString();

            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = $"Product is deleted";
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

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public IActionResult ViewInRefrigerator(ProductModel product)
    {
        return RedirectToAction("ProductItem", new { productId = product.Id, productName = product.Name, unitType = product.UnitType, productAction = ProductAction.View.ToString() });
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public IActionResult ViewProductHistory(ProductModel product)
    {
        return RedirectToAction("ProductHistory", new { productId = product.Id, productName = product.Name });
    }
    
    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public IActionResult AddToRefrigerator(ProductModel product)
    {
        return RedirectToAction("ProductItem", new { productId = product.Id, productName = product.Name, unitType = product.UnitType, productAction = ProductAction.Add.ToString() });
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public IActionResult TakeFromRefrigerator(ProductModel product)
    {
        return RedirectToAction("ProductItem", new { productId = product.Id, productName = product.Name, unitType = product.UnitType, productAction = ProductAction.TakePartOf.ToString() });
    }
}
