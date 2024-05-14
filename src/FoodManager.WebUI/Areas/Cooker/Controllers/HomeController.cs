using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Cooker.Controllers;

[Area("Cooker")]
[Authorize(Roles = "Cooker")]
public class HomeController : Controller
{
    [Route("{area}")]
    [Route("{area}/{controller}")]
    [Route("{area}/{controller}/{action}")]
    public IActionResult Index()
    {
        return View();
    }
}
