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
public class ShopsController : ControllerBase
{
    private readonly IShopsService _shopsService;
    private readonly IMapper _mapper;

    public ShopsController(IShopsService shopsService,
        IMapper mapper)
    {
        _shopsService = shopsService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<ShopModel>> Get([FromQuery] ShopGetModel model)
    {
        try
        {
            var candidate = await _shopsService.GetAsync(model.Id);
            var result = _mapper.Map<ShopModel>(candidate);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAll")]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<IEnumerable<ShopModel>>> GetAll([FromQuery] bool onlyActive)
    {
        try
        {
            var candidates = await _shopsService.GetAllAsync(onlyActive);
            var result = _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopModel>>(candidates);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public async Task<ActionResult<Guid>> Create([FromQuery] ShopCreateModel model)
    {
        try
        {
            var shopDto = _mapper.Map<Shop>(model);
            var result = await _shopsService.CreateAsync(shopDto);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public ActionResult Update([FromQuery] ShopModel model)
    {
        try
        {
            var shopDto = _mapper.Map<Shop>(model);
            _shopsService.Update(shopDto);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    public ActionResult Delete([FromQuery] ShopDeleteModel model)
    {
        try
        {
            _shopsService.Delete(model.Id);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}