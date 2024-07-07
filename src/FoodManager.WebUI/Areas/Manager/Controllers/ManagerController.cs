using FoodManager.WebUI.Areas.Administrator.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Manager.Controllers;

[Area("Manager")]
[Authorize(Roles = "Manager")]

public sealed class ManagerController : Abstractions.ControllerBase
{
    private static readonly string ExpireProductsReportUrl = "/api/Report/GenerateExpireProductsReport/";
    public ManagerController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    [Route("{area}")]
    [Route("{area}/{controller}")]
    [Route("{area}/{controller}/{action}")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> GenerateReport(int daysBeforeExpired)
    {
        if (!ModelState.IsValid)
        {        
            BadRequest();
        }

        var httpClient = CreatePlannerServiceClient();

        UserCreatedResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(ExpireProductsReportUrl + $"?daysBeforeExpired={daysBeforeExpired}");

            response = await responseMessage.Content.ReadFromJsonAsync<UserCreatedResponse>();
            responseMessage.EnsureSuccessStatusCode();

            TempData["Message"] = $"Report is created. Will notify by telegram.";
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("Index");
    }
}
