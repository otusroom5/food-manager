using FoodManager.Shared.Types;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.WebApi.Models.Extensions;
using FoodStorage.WebApi.Models.RecipeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Authorize(Roles = UserRole.Administration)]
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
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateRecipeModel recipe)
    {
        Recipe recipeToCreate = recipe.ToEntity();
        RecipeId id = await _recipeService.CreateAsync(recipeToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить рецепт по идентификатору
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>Рецепт</returns>
    [HttpGet("GetById/{recipeId}")]
    public async Task<ActionResult<RecipeModel>> GetByIdAsync(Guid recipeId)
    {
        Recipe recipe = await _recipeService.GetByIdAsync(RecipeId.FromGuid(recipeId));
        RecipeModel result = recipe.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить рецепт по имени
    /// </summary>
    /// <param name="recipeName">Наименование рецепта</param>
    /// <returns>Рецепт</returns>
    [HttpGet("GetByName/{recipeName}")]
    public async Task<ActionResult<RecipeModel>> GetByNameAsync(string recipeName)
    {
        Recipe recipe = await _recipeService.GetByNameAsync(RecipeName.FromString(recipeName));
        RecipeModel result = recipe.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты
    /// </summary>
    /// <returns>Список всех рецептов</returns>
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<RecipeModel>>> GetAllAsync()
    {
        IEnumerable<Recipe> recipes = await _recipeService.GetAllAsync();
        List<RecipeModel> result = recipes.Select(r => r.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты, где есть указанный продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список рецептов</returns>
    [HttpGet("GetByProductId/{productId}")]
    public async Task<ActionResult<List<RecipeModel>>> GetByProductIdAsync(Guid productId)
    {
        IEnumerable<Recipe> recipes = await _recipeService.GetByProductIdAsync(ProductId.FromGuid(productId));
        List<RecipeModel> result = recipes.Select(r => r.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Изменить рецепт
    /// </summary>
    /// <param name="recipe">Модель рецепта</param>
    /// <returns>ок</returns>
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateAsync(RecipeModel recipe)
    {
        Recipe recipeToUpdate = recipe.ToEntity();
        await _recipeService.UpdateAsync(recipeToUpdate);

        return Ok();
    }

    /// <summary>
    /// Удалить рецепт
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{recipeId}")]
    public async Task<ActionResult> DeleteAsync(Guid recipeId)
    {
        await _recipeService.DeleteAsync(RecipeId.FromGuid(recipeId));

        return Ok();
    }
}
