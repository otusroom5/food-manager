using AutoMapper;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.WebAPI.Models;
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

    [HttpGet("Get")]
    public ActionResult<ShopModel> Get([FromQuery] ShopGetModel model)
    {
        try
        {
            var candidate = _shopsService.Get(model.Id);
            var result = _mapper.Map<ShopModel>(candidate);

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

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<ShopModel>> GetAll([FromQuery] bool onlyActive)
    {
        try
        {
            var candidates = _shopsService.GetAll(onlyActive);
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

    [HttpPost("Create")]
    public ActionResult<Guid> Create([FromQuery] ShopCreateModel model)
    {
        try
        {
            var shopDto = _mapper.Map<Shop>(model);
            var result = _shopsService.Create(shopDto);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Update")]
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

    [HttpDelete("Delete")]
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