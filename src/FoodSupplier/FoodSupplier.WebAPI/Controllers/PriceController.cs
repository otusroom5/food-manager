using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodSupplier.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PriceController : ControllerBase
{
    private readonly IPricesService _pricesService;

    public PriceController(IPricesService pricesService)
    {
        _pricesService = pricesService;
    }

    [HttpGet("Get")]
    public ActionResult<PriceModel> Get([FromQuery] PriceEntryGetModel model)
    {
        try
        {
            var candidate = _pricesService.Get(model.Id);

            return Ok(candidate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetLastByProduct")]
    public ActionResult<PriceModel> GetLastByProduct([FromQuery] ProductGetModel model)
    {
        try
        {
            var candidate = _pricesService.GetLast(model.Id);

            return Ok(candidate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}