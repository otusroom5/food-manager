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
        Guid id = await _productItemService.CreateAsync(productItem, UserId.ToGuid());

        return Ok(id);
    }

    /// <summary>
    /// Получить единицу продукта по идентификатору
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>Единица продукта</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetById/{productItemId}")]
    public async Task<ActionResult<ProductItemViewModel>> GetByIdAsync(Guid productItemId)
    {
        ProductItemViewModel result = await _productItemService.GetByIdAsync(productItemId);

        return Ok(result);
    }

    /// <summary>
    /// Получить единицы продукта по идентификатору самого продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetByProductId/{productId}")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetByProductIdAsync(Guid productId)
    {
        List<ProductItemViewModel> result = await _productItemService.GetByProductIdAsync(productId);

        return Ok(result);
    }

    /// <summary>
    /// Получить все единицы продуктов
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetAllAsync()
    {
        List<ProductItemViewModel> result = await _productItemService.GetAllAsync();

        return Ok(result);
    }

    /// <summary>
    /// Получить список единиц продуктов с истекающим сроком годности, если 0 - то с истекшим
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey")] // Need to check how config to work with Roles together
    [HttpGet("GetExpiredProductItems")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetExpireProductItemsAsync(int daysBeforeExpired = 0)
    {
        List<ProductItemViewModel> result = await _productItemService.GetExpireProductItemsAsync(daysBeforeExpired);

        return Ok(result);
    }

    /// <summary>
    /// Получить список заканчивающихся единиц продуктов в холодильнике
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetEndingProductItems")]
    public async Task<ActionResult<List<ProductItemViewModel>>> GetEndingProductItemsAsync()
    {
        List<ProductItemViewModel> result = await _productItemService.GetEndingProductItemsAsync();

        return Ok(result);
    }

    /// <summary>
    /// Взять часть продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="count">Количество продукта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("TakePartOf/{productId}/{count}")]
    public async Task<ActionResult> TakePartOfAsync(Guid productId, int count)
    {
        await _productItemService.TakePartOfAsync(productId, count, UserId.ToGuid());

        return Ok();
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
        await _productItemService.WriteOffAsync(productItemIds, UserId.ToGuid());

        return Ok();
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
        await _productItemService.DeleteAsync(productItemId);

        return Ok();
    }
}
