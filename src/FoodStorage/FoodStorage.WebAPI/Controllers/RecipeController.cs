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
public class RecipeController : BaseController
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    /// <summary>
    /// Создать рецепт
    /// </summary>
    /// <param name="recipet">Модель рецепта</param>
    /// <returns>Идентификатор созданного рецепта</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateAsync(RecipeCreateRequestModel recipe)
    {
        Guid id = await _recipeService.CreateAsync(recipe);

        return Ok(id);
    }

    /// <summary>
    /// Получить рецепт по идентификатору
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>Рецепт</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpGet("GetById/{recipeId}")]
    public async Task<ActionResult<RecipeViewModel>> GetByIdAsync(Guid recipeId)
    {
        RecipeViewModel result = await _recipeService.GetByIdAsync(recipeId);

        return Ok(result);
    }

    /// <summary>
    /// Получить рецепт по имени
    /// </summary>
    /// <param name="recipeName">Наименование рецепта</param>
    /// <returns>Рецепт</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetByName/{recipeName}")]
    public async Task<ActionResult<RecipeViewModel>> GetByNameAsync(string recipeName)
    {
        RecipeViewModel result = await _recipeService.GetByNameAsync(recipeName);

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты
    /// </summary>
    /// <returns>Список всех рецептов</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<RecipeViewModel>>> GetAllAsync()
    {
        List<RecipeViewModel> result = await _recipeService.GetAllAsync();

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты, где есть указанный продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список рецептов</returns>
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
    [HttpGet("GetByProductId/{productId}")]
    public async Task<ActionResult<List<RecipeViewModel>>> GetByProductIdAsync(Guid productId)
    {
        List<RecipeViewModel> result = await _recipeService.GetByProductIdAsync(productId);

        return Ok(result);
    }

    /// <summary>
    /// Изменить рецепт
    /// </summary>
    /// <param name="recipe">Модель рецепта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateAsync(RecipeUpdateRequestModel recipe)
    {
        await _recipeService.UpdateAsync(recipe);

        return Ok();
    }

    /// <summary>
    /// Удалить рецепт
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>ок</returns>
    [Authorize(Roles = UserRole.Cooker)]
    [HttpDelete("Delete/{recipeId}")]
    public async Task<ActionResult> DeleteAsync(Guid recipeId)
    {
        await _recipeService.DeleteAsync(recipeId);

        return Ok();
    }
}
