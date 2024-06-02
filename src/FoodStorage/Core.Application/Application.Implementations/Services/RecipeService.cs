using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(IRecipeRepository recipeRepository, IProductRepository productRepository, ILogger<RecipeService> logger)
    {
        _recipeRepository = recipeRepository;
        _productRepository = productRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public async Task<RecipeId> CreateAsync(Recipe recipe)
    {
        try
        {
            // проверка на существование рецепта с таким же именем
            var recipeWithSameName = await _recipeRepository.FindByNameAsync(recipe.Name);
            if (recipeWithSameName is not null)
            {
                throw new ApplicationLayerException($"{nameof(Recipe)} with same name '{recipe.Name}' is already exists");
            }

            // проверка существования продуктов, кот. будут в рецепте
            foreach (var recipeItem in recipe.Positions)
            {
                Product product = await _productRepository.FindByIdAsync(recipeItem.ProductId);
                if (product is null)
                {
                    throw new EntityNotFoundException(nameof(Product), recipeItem.ProductId.ToString());
                }
            }

            await _recipeRepository.CreateAsync(recipe);

            return recipe.Id;
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<Recipe> GetByIdAsync(RecipeId recipeId)
    {
        try
        {
            Recipe recipe = await _recipeRepository.FindByIdAsync(recipeId);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeId.ToString());
            }

            return recipe;
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<Recipe> GetByNameAsync(RecipeName recipeName)
    {
        try
        {
            Recipe recipe = await _recipeRepository.FindByNameAsync(recipeName);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeName.ToString());
            }

            return recipe;
        }
        catch (Exception exception)
        {
            LogError("GetByName", exception);
            throw;
        }
    }

    public async Task<IEnumerable<Recipe>> GetByProductIdAsync(ProductId productId) => await _recipeRepository.GetByProductIdAsync(productId);

    public async Task<IEnumerable<Recipe>> GetAllAsync() => await _recipeRepository.GetAllAsync();

    public async Task UpdateAsync(Recipe recipe)
    {
        try
        {
            // проверка на существование этого рецепта в базе
            var recipeFromBase = await _recipeRepository.FindByIdAsync(recipe.Id);

            if (recipeFromBase is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipe.Id.ToString());
            }

            await _recipeRepository.ChangeAsync(recipe);
        }
        catch (Exception exception)
        {
            LogError("Update", exception);
            throw;
        }
    }

    public async Task DeleteAsync(RecipeId recipeId)
    {
        try
        {
            // проверка на существование этого рецепта в базе
            var recipe = await _recipeRepository.FindByIdAsync(recipeId);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeId.ToString());
            }

            await _recipeRepository.DeleteAsync(recipe);
        }
        catch (Exception exception)
        {
            LogError("Delete", exception);
            throw;
        }
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
