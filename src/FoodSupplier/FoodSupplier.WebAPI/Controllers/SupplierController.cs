using FoodManager.Shared.Types;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult> Produce()
    {
        try
        {
            await _supplierService.ProduceAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProduceSingle")]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult> ProduceSingle([FromQuery] SingleProduceModel model)
    {
        try
        {
            await _supplierService.ProduceAsync(model.ShopId, model.ProductId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}