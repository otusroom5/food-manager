using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Cooker.Controllers;

[Area("Cooker")]
[Authorize(Roles = "Cooker")]
public sealed class CookerController : Abstractions.ControllerBase
{
    public CookerController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    [Route("{area}")]
    [Route("{area}/{controller}")]
    [Route("{area}/{controller}/{action}")]
    public IActionResult Index()
    {
        
        return View();
    }
}
