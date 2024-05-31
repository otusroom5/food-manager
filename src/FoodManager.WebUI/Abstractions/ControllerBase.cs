using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FoodManager.WebUI.Abstractions;

public abstract class ControllerBase : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    protected ControllerBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected HttpClient CreateUserAuthServiceClient()
    {
        return _httpClientFactory.CreateClient("UserAuthApi");
    }

    protected HttpClient CreateStorageServiceClient()
    {
        return _httpClientFactory.CreateClient("FoodStorageApi");
    }

    protected HttpClient CreateSupplierServiceClient()
    {
        return _httpClientFactory.CreateClient("FoodSupplierApi");
    }

    protected HttpClient CreatePlannerServiceClient()
    {
        return _httpClientFactory.CreateClient("FoodPlannerApi");
    }

    protected Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty);
    }
}
