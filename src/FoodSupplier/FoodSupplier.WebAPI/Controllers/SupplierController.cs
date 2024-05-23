using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodSupplier.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("Produce")]
    public ActionResult Produce()
    {
        try
        {
            _supplierService.Produce();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProduceSingle")]
    public ActionResult ProduceSingle([FromQuery] SingleProduceModel model)
    {
        try
        {
            _supplierService.Produce(model.ShopId, model.ProductId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}