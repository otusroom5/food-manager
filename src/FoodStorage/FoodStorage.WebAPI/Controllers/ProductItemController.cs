using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.WebApi.Models.Extensions;
using FoodStorage.WebApi.Models.ProductItemModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ProductItemController : ControllerBase
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
    public ActionResult<Guid> Create(CreateProductItemModel productItem)
    {
        ProductItem productItemToCreate = productItem.ToEntity();
        ProductItemId id = _productItemService.Create(productItemToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить единицу продукта по идентификатору
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>Единица продукта</returns>
    [HttpGet("GetById/{productItemId}")]
    public ActionResult<ProductItemModel> GetById(Guid productItemId)
    {
        ProductItem productItem = _productItemService.GetById(ProductItemId.FromGuid(productItemId));
        ProductItemModel result = productItem.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить единицы продукта по идентификатору самого продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetByProductId/{productId}")]
    public ActionResult<List<ProductItemModel>> GetByProductId(Guid productId)
    {
        IEnumerable<ProductItem> productItems = _productItemService.GetByProductId(ProductId.FromGuid(productId));
        List<ProductItemModel> result = productItems.Select(pi => pi.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить все единицы продуктов
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetAll")]
    public ActionResult<List<ProductItemModel>> GetAll()
    {
        IEnumerable<ProductItem> productItems = _productItemService.GetAll();
        List<ProductItemModel> result = productItems.Select(pi => pi.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить список единиц продуктов с истекшим сроком годности
    /// </summary>
    /// <returns>Список единиц продукта</returns>
    [HttpGet("GetExpiredProductItems")]
    public ActionResult<List<ProductItemModel>> GetExpiredProductItems()
    {
        IEnumerable<ProductItem> productItems = _productItemService.GetExpiredProductItems();
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
    public ActionResult TakePartOf(Guid productId, int count)
    {
        UserId userId = UserId.FromGuid(Guid.NewGuid()); //--------------
        _productItemService.TakePartOf(ProductId.FromGuid(productId), count, userId);

        return Ok();
    }

    /// <summary>
    /// Списать указанные единицы продукта
    /// </summary>
    /// <param name="productItemIds">Идентификаторы единиц продукта</param>
    /// <returns>ок</returns>
    [HttpPost("WriteOff")]
    public ActionResult WriteOff(List<Guid> productItemIds)
    {
        UserId userId = UserId.FromGuid(Guid.NewGuid()); //--------------
        IEnumerable<ProductItemId> ids = productItemIds.Select(ProductItemId.FromGuid);
        _productItemService.WriteOff(ids, userId);

        return Ok();
    }

    /// <summary>
    /// Удалить единицу продукта
    /// </summary>
    /// <param name="productItemId">Идентификатор единицы продукта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{productItemId}")]
    public ActionResult Delete(Guid productItemId)
    {
        _productItemService.Delete(ProductItemId.FromGuid(productItemId));

        return Ok();
    }
}
