using FoodManager.Shared.Types;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.WebApi.Models.Extensions;
using FoodStorage.WebApi.Models.ProductItemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Authorize(Roles = UserRole.Administration)]
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
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateProductItemModel productItem)
    {
        ProductItem productItemToCreate = productItem.ToEntity(UserId);
        ProductItemId id = await _productItemService.CreateAsync(productItemToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить единицу продукта по идентификатору
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>Единица продукта</returns>
    [HttpGet("GetById/{productItemId}")]
    public async Task<ActionResult<ProductItemModel>> GetByIdAsync(Guid productItemId)
    {
        ProductItem productItem = await _productItemService.GetByIdAsync(ProductItemId.FromGuid(productItemId));
        ProductItemModel result = productItem.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить единицы продукта по идентификатору самого продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetByProductId/{productId}")]
    public async Task<ActionResult<List<ProductItemModel>>> GetByProductIdAsync(Guid productId)
    {
        IEnumerable<ProductItem> productItems = await _productItemService.GetByProductIdAsync(ProductId.FromGuid(productId));
        List<ProductItemModel> result = productItems.Select(pi => pi.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить все единицы продуктов
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductItemModel>>> GetAllAsync()
    {
        IEnumerable<ProductItem> productItems = await _productItemService.GetAllAsync();
        List<ProductItemModel> result = productItems.Select(pi => pi.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить список единиц продуктов с истекшим сроком годности
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetExpiredProductItems")]
    public async Task<ActionResult<List<ProductItemModel>>> GetExpiredProductItemsAsync()
    {
        IEnumerable<ProductItem> productItems = await _productItemService.GetExpireProductItemsAsync();
        List<ProductItemModel> result = productItems.Select(pi => pi.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Взять часть продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="count">Количество продукта</param>
    /// <returns>ок</returns>
    [HttpPost("TakePartOf/{productId}/{count}")]
    public async Task<ActionResult> TakePartOfAsync(Guid productId, int count)
    {
        await _productItemService.TakePartOfAsync(ProductId.FromGuid(productId), count, UserId);

        return Ok();
    }

    /// <summary>
    /// Списать указанные единицы продукта
    /// </summary>
    /// <param name="productItemIds">Идентификаторы единиц продукта</param>
    /// <returns>ок</returns>
    [HttpPost("WriteOff")]
    public async Task<ActionResult> WriteOffAsync(List<Guid> productItemIds)
    {
        IEnumerable<ProductItemId> ids = productItemIds.Select(ProductItemId.FromGuid);
        await _productItemService.WriteOffAsync(ids, UserId);

        return Ok();
    }

    /// <summary>
    /// Удалить единицу продукта
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{productItemId}")]
    public async Task<ActionResult> DeleteAsync(Guid productItemId)
    {
        await _productItemService.DeleteAsync(ProductItemId.FromGuid(productItemId));

        return Ok();
    }
}
