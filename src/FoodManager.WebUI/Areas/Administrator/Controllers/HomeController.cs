using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Administrator.Controllers;

[Area("Administrator")]
[Authorize(Roles = "Administrator")]
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
