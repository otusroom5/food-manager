using FoodManager.Shared.Types;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class UnitController : BaseController
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    /// <summary>
    /// Создать единицу измерения
    /// </summary>
    /// <param name="unit">Модель единицы измерения</param>
    /// <returns>Идентификатор созданной единицы измерения</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("Create")]
    public async Task<ActionResult<string>> CreateAsync(UnitCreateRequestModel unit)
    {
        try
        {
            string id = await _unitService.CreateAsync(unit);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить единицу измерения по идентификатору
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <returns>Единица измерения</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetById/{unitId}")]
    public async Task<ActionResult<UnitViewModel>> GetByIdAsync(string unitId)
    {
        try
        {
            UnitViewModel result = await _unitService.GetByIdAsync(unitId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить единицу измерения по типу (группе)
    /// </summary>
    /// <param name="unitType">Тип единицы измерения</param>
    /// <returns>Единица измерения</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetByUnitType/{unitType}")]
    public async Task<ActionResult<List<UnitViewModel>>> GetByUnitTypeAsync(string unitType)
    {
        try
        {
            List<UnitViewModel> result = await _unitService.GetByTypeAsync(unitType);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить все единицы измерения
    /// </summary>
    /// <returns>Список единиц измерения</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductViewModel>>> GetAllAsync()
    {
        try
        {
            List<UnitViewModel> result = await _unitService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить все типы единиц измерения
    /// </summary>
    /// <returns>Список типов единиц измерения</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetAllUnitTypes")]
    public async Task<ActionResult<List<string>>> GetAllUnitTypesAsync()
    {
        try
        {
            List<string> result = await _unitService.GetAllTypesAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить единицу измерения
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpDelete("Delete/{unitId}")]
    public async Task<ActionResult> DeleteAsync(string unitId)
    {
        try
        {
            await _unitService.DeleteAsync(unitId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}