using FoodManager.WebUI.Areas.Cooker.Models;
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
        return View(new ProductIndexModel()
        {
            Products = new ProductModel[]
            {
                new ProductModel()
                {
                    Id = "0",
                    Name = "Milk",
                    UnitType = "кг",
                    BestBeforeDate = "2023-01-01",
                    MinAmountPerDay = "10"
                }
            }
        });
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> CreateProduct(ProductCreateModel model)
    {
        return RedirectToAction("Index");
    }


    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public IActionResult UpdateProduct(string id)
    {
        return View("Index", new ProductIndexModel()
        {
            Products = new ProductModel[]
            {
                new ProductModel()
                {
                    Id = "0",
                    Name = "Milk",
                    UnitType = "кг",
                    BestBeforeDate = "2023-01-01",
                    MinAmountPerDay = "10"
                }
            },
            Product = new ProductModel()
            {
                Id = "0",
                Name = "Milk",
                UnitType = "кг",
                BestBeforeDate = "2023-01-01",
                MinAmountPerDay = "10"
            }
        });
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public IActionResult UpdateProduct(ProductUpdateModel product)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public IActionResult DeleteProduct(Guid id)
    {
        return RedirectToAction("Index");
    }
}
