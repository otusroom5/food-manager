using Microsoft.AspNetCore.Mvc;
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

    protected HttpClient CreateServiceHttpClient(string serviceName, bool useToken = true)
    {
        var httpClient = _httpClientFactory.CreateClient(serviceName);
        if (useToken)
        {
            string jwtToken = GetToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $" {jwtToken}");
        }
        return httpClient;
    }

    private string GetToken()
    {
        return HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.UserData))?.Value ?? string.Empty;
    }

    protected Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty);
    }
}
