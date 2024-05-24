using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Manager.Controllers;

[Area("Manager")]
[Authorize(Roles = "Manager")]

public sealed class ManagerController : Abstractions.ControllerBase
{
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
}
