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
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateAsync(ProductCreateRequestModel product)
    {
        try
        {
            Guid id = await _productService.CreateAsync(product);
            return Ok(id);
        }
        catch (Exception ex) 
        { 
            return BadRequest(ex.Message); 
        }
    }

    /// <summary>
    /// Получить продукт по идентификатору
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Продукт</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetById/{productId}")]
    public async Task<ActionResult<ProductViewModel>> GetByIdAsync(Guid productId)
    {
        try
        {
            ProductViewModel result = await _productService.GetByIdAsync(productId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Получить продукт по имени
    /// </summary>
    /// <param name="productName">Наенование продукта</param>
    /// <returns>Продукт</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetByName/{productName}")]
    public async Task<ActionResult<ProductViewModel>> GetByNameAsync(string productName)
    {
        try
        {
            ProductViewModel result = await _productService.GetByNameAsync(productName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить все продукты
    /// </summary>
    /// <returns>Список продуктов</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey",Roles = UserRole.Cooker)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductViewModel>>> GetAllAsync()
    {
        try
        {
            List<ProductViewModel> result = await _productService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>ок</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpDelete("Delete/{productId}")]
    public async Task<ActionResult> DeleteAsync(Guid productId)
    {
        try
        {
            await _productService.DeleteAsync(productId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
