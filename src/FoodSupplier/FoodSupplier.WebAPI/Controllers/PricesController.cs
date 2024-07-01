using AutoMapper;
using FoodManager.Shared.Types;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSupplier.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PricesController : ControllerBase
{
    private readonly IPricesService _pricesService;
    private readonly IMapper _mapper;

    public PricesController(IPricesService pricesService, IMapper mapper)
    {
        _pricesService = pricesService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<PriceModel>> Get([FromQuery] PriceEntryGetModel model)
    {
        try
        {
            var candidate = await _pricesService.GetAsync(model.Id);
            var result = _mapper.Map<PriceModel>(candidate);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetLastByProduct")]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<PriceModel>> GetLastByProduct([FromQuery] ProductGetModel model)
    {
        try
        {
            var candidate = await _pricesService.GetLastAsync(model.Id);
            var result = _mapper.Map<PriceModel>(candidate);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAllByProduct")]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<IEnumerable<PriceModel>>> GetAllByProduct([FromQuery] ProductGetModel model)
    {
        try
        {
            var candidate = await _pricesService.GetAllAsync(model.Id);
            var result = _mapper.Map<IEnumerable<PriceEntry>, IEnumerable<PriceModel>>(candidate);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}