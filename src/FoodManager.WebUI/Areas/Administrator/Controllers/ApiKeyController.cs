using FoodManager.WebUI.Areas.Administrator.Contracts.Responses;
using FoodManager.WebUI.Areas.Administrator.Models;
using FoodManager.WebUI.Contracts;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Administrator.Controllers;

public sealed partial class AdministratorController : Abstractions.ControllerBase
{
    private readonly static string ApiKeyUrl = "/api/v1/ApiKeys";

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> ApiKeys()
    {
        var httpClient = CreateUserAuthServiceClient();

        ApiKeyResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(ApiKeyUrl);

            response = await responseMessage.Content.ReadFromJsonAsync<ApiKeyResponse>();
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return View(new ApiKeyIndexModel()
        {
            ApiKeys = response?.Data.Select(f => f.ToModel()).ToArray()
        });
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> CreateKey(ApiKeyCreateModel model)
    {
        var httpClient = CreateUserAuthServiceClient();

        ApiKeyCreateResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsync(ApiKeyUrl, JsonContent.Create(model.ToRequest()));
            response = await responseMessage.Content.ReadFromJsonAsync<ApiKeyCreateResponse>();
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("ApiKeys");
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> DeleteKey(string id)
    {
        if (!Guid.TryParse(id, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateUserAuthServiceClient();

        ResponseBase response = null;
        try
        {
            Uri requestUri = new UriBuilder(httpClient.BaseAddress)
            {
                Path = ApiKeyUrl,
                Query = QueryString.Create("KeyId", id).Value
            }.Uri;

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);

            response = await responseMessage.Content.ReadFromJsonAsync<ResponseBase>();
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("ApiKeys");
    }
}
