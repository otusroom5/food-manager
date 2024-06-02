using FoodManager.Shared.Types;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.WebApi.Models.Extensions;
using FoodStorage.WebApi.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Authorize(Roles = UserRole.Administration)]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ProductController : BaseController
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
    public async Task<ActionResult<Guid>> CreateAsync(CreateProductModel product)
    {
        Product productToCreate = product.ToEntity();
        ProductId id = await _productService.CreateAsync(productToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить продукт по идентификатору
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetById/{productId}")]
    public async Task<ActionResult<ProductModel>> GetByIdAsync(Guid productId)
    {
        Product product = await _productService.GetByIdAsync(ProductId.FromGuid(productId));
        ProductModel result = product.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить продукт по имени
    /// </summary>
    /// <param name="productName">Наенование продукта</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetByName/{productName}")]
    public async Task<ActionResult<ProductModel>> GetByNameAsync(string productName)
    {
        Product product = await _productService.GetByNameAsync(ProductName.FromString(productName));
        ProductModel result = product.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить все продукты
    /// </summary>
    /// <returns>Список продуктов</returns>
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductModel>>> GetAllAsync()
    {
        IEnumerable<Product> products = await _productService.GetAllAsync();
        List<ProductModel> result = products.Select(p => p.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Удалить продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{productId}")]
    public async Task<ActionResult> DeleteAsync(Guid productId)
    {
        await _productService.DeleteAsync(ProductId.FromGuid(productId));

        return Ok();
    }
}
