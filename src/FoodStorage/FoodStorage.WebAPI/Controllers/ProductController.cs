using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.WebApi.Models;
using FoodStorage.WebApi.Models.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Создать продукт
    /// </summary>
    /// <param name="product">Модель продукта</param>
    /// <returns>Идентификатор созданного продукта</returns>
    [HttpPost("Create")]
    public ActionResult<Guid> Create(ProductModel product)
    {
        Product productToCreate = product.ToEntity();
        ProductId id = _productService.Create(productToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить продукт по идентификатору
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetById/{productId}")]
    public ActionResult<ProductModel> GetById(Guid productId)
    {
        Product product = _productService.GetById(ProductId.FromGuid(productId));
        ProductModel result = product.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить продукт по имени
    /// </summary>
    /// <param name="productName">Наенование продукта</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetByName/{productName}")]
    public ActionResult<ProductModel> GetByName(string productName)
    {
        Product product = _productService.GetByName(ProductName.FromString(productName));
        ProductModel result = product.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить все продукты
    /// </summary>
    /// <returns>Список продуктов</returns>
    [HttpGet("GetAll")]
    public ActionResult<List<ProductModel>> GetAll()
    {
        IEnumerable<Product> products = _productService.GetAll();
        List<ProductModel> result = products.Select(p => p.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Удалить продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{productId}")]
    public ActionResult Delete(Guid productId)
    {
        _productService.Delete(ProductId.FromGuid(productId));

        return Ok();
    }
}
