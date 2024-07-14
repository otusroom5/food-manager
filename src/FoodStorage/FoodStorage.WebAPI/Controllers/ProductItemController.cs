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
public class ProductItemController : BaseController
{
    private readonly IProductItemService _productItemService;

    public ProductItemController(IProductItemService productItemService)
    {
        _productItemService = productItemService;
    }

    /// <summary>
    /// Создать единицу продукта
    /// </summary>
    /// <param name="productItem">Модель единицы продукта</param>
    /// <returns>Идентиикатор созданной единицы продукта</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateAsync(ProductItemCreateRequestModel productItem)
    {
        try
        {
            Guid id = await _productItemService.CreateAsync(productItem, UserId.ToGuid());
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить единицу продукта по идентификатору
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <param name="unit">Единица измерения</param>
    /// <returns>Единица продукта</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetById/{productItemId}/{unit}")]
    public async Task<ActionResult<ProductItemViewModel>> GetByIdAsync(Guid productItemId, string unit)
    {
        try
        {
            ProductItemViewModel result = await _productItemService.GetByIdAsync(productItemId, unit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить единицы продукта по идентификатору самого продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="unit">Единица измерения</param>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetByProductId/{productId}/{unit}")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetByProductIdAsync(Guid productId, string unit)
    {
        try
        {
            List<ProductItemViewModel> result = await _productItemService.GetByProductIdAsync(productId, unit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить все единицы продуктов
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetAllAsync()
    {
        try
        {
            List<ProductItemViewModel> result = await _productItemService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить список единиц продуктов с истекающим сроком годности, если 0 - то с истекшим
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    [HttpGet("GetExpiredProductItems")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetExpireProductItemsAsync(int daysBeforeExpired = 0)
    {
        try
        {
            List<ProductItemViewModel> result = await _productItemService.GetExpireProductItemsAsync(daysBeforeExpired);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить список заканчивающихся единиц продуктов в холодильнике
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetEndingProductItems")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetEndingProductItemsAsync()
    {
        try
        {
            List<ProductItemViewModel> result = await _productItemService.GetEndingProductItemsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Взять часть продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="count">Количество продукта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("TakePartOf")]
    public async Task<ActionResult> TakePartOfAsync(ProductItemTakePartOfRequestModel request)
    {
        try
        {
            await _productItemService.TakePartOfAsync(request, UserId.ToGuid());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Списать указанные единицы продукта
    /// </summary>
    /// <param name="productItemIds">Идентификаторы единиц продукта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("WriteOff")]
    public async Task<ActionResult> WriteOffAsync(List<Guid> productItemIds)
    {
        try
        {
            await _productItemService.WriteOffAsync(productItemIds, UserId.ToGuid());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить единицу продукта
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpDelete("Delete/{productItemId}")]
    public async Task<ActionResult> DeleteAsync(Guid productItemId)
    {
        try
        {
            await _productItemService.DeleteAsync(productItemId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Тех метод для занесения единиц продукта в холодильник
    /// </summary>
    /// <returns>ок</returns>
    [HttpPost("TechPutProductItems")]
    public async Task<ActionResult> TechPutProductItemsAsync()
    {
        try
        {
            await _productItemService.TechPutProductItemsAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Тех метод для удаления записей всех
    /// </summary>
    /// <returns>ок</returns>
    [HttpDelete("TechDeleteProductItemsAsync")]
    public async Task<ActionResult> TechDeleteProductItemsAsync()
    {
        try
        {
            await _productItemService.TechPutProductItemsAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
