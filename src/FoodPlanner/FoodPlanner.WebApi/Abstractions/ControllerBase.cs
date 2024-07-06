using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.WebApi.Abstractions;

public abstract class ControllerBase : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    protected ControllerBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected HttpClient CreateStorageServiceClient()
    {
        return _httpClientFactory.CreateClient("FoodStorageApi");
    }

    protected HttpClient CreateSupplierServiceClient()
    {
        return _httpClientFactory.CreateClient("FoodSupplierApi");
    }
}