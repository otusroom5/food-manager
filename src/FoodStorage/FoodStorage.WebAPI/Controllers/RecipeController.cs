using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.WebApi.Models;
using FoodStorage.WebApi.Models.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class RecipeController : ControllerBase
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
    public ActionResult<Guid> Create(RecipeModel recipe)
    {
        Recipe recipeToCreate = recipe.ToEntity();
        RecipeId id = _recipeService.Create(recipeToCreate);

        return Ok(id.ToGuid());
    }

    /// <summary>
    /// Получить рецепт по идентификатору
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>Рецепт</returns>
    [HttpGet("GetById/{recipeId}")]
    public ActionResult<RecipeModel> GetById(Guid recipeId)
    {
        Recipe recipe = _recipeService.GetById(RecipeId.FromGuid(recipeId));
        RecipeModel result = recipe.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить рецепт по имени
    /// </summary>
    /// <param name="recipeName">Наименование рецепта</param>
    /// <returns>Рецепт</returns>
    [HttpGet("GetByName/{recipeName}")]
    public ActionResult<RecipeModel> GetByName(string recipeName)
    {
        Recipe recipe = _recipeService.GetByName(RecipeName.FromString(recipeName));
        RecipeModel result = recipe.ToModel();

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты
    /// </summary>
    /// <returns>Список всех рецептов</returns>
    [HttpGet("GetAll")]
    public ActionResult<List<RecipeModel>> GetAll()
    {
        IEnumerable<Recipe> Recipes = _recipeService.GetAll();
        List<RecipeModel> result = Recipes.Select(r => r.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить все рецепты, где есть указанный продукт
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Список рецептов</returns>
    [HttpGet("GetByProductId/{productId}")]
    public ActionResult<List<RecipeModel>> GetByProductId(Guid productId)
    {
        IEnumerable<Recipe> Recipes = _recipeService.GetByProductId(ProductId.FromGuid(productId));
        List<RecipeModel> result = Recipes.Select(r => r.ToModel()).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Изменить рецепт
    /// </summary>
    /// <param name="recipe">Модель рецепта</param>
    /// <returns>ок</returns>
    [HttpPut("Update")]
    public ActionResult Update(RecipeModel recipe)
    {
        Recipe recipeToUpdate = recipe.ToEntity();
        _recipeService.Update(recipeToUpdate);

        return Ok();
    }

    /// <summary>
    /// Удалить рецепт
    /// </summary>
    /// <param name="recipeId">Идентификатор рецепта</param>
    /// <returns>ок</returns>
    [HttpDelete("Delete/{recipeId}")]
    public ActionResult Delete(Guid recipeId)
    {
        _recipeService.Delete(RecipeId.FromGuid(recipeId));

        return Ok();
    }
}
