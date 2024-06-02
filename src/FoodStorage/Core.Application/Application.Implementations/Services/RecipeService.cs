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

    public RecipeId Create(Recipe recipe)
    {
        try
        {
            // проверка на существование рецепта с таким же именем
            var recipeWithSameName = _recipeRepository.FindByName(recipe.Name);
            if (recipeWithSameName is not null)
            {
                throw new ApplicationLayerException($"{nameof(Recipe)} with same name '{recipe.Name}' is already exists");
            }

            // проверка существования продуктов, кот. будут в рецепте
            foreach (var recipeItem in recipe.Positions)
            {
                Product product = _productRepository.FindById(recipeItem.ProductId);
                if (product is null)
                {
                    throw new EntityNotFoundException(nameof(Product), recipeItem.ProductId.ToString());
                }
            }

            _recipeRepository.Create(recipe);

            return recipe.Id;
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public Recipe GetById(RecipeId recipeId)
    {
        try
        {
            Recipe recipe = _recipeRepository.FindById(recipeId);

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

    public Recipe GetByName(RecipeName recipeName)
    {
        try
        {
            Recipe recipe = _recipeRepository.FindByName(recipeName);

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

    public IEnumerable<Recipe> GetByProductId(ProductId productId) => _recipeRepository.GetByProductId(productId);

    public IEnumerable<Recipe> GetAll() => _recipeRepository.GetAll();

    public void Update(Recipe recipe)
    {
        try
        {
            // проверка на существование этого рецепта в базе
            var recipeFromBase = _recipeRepository.FindById(recipe.Id);

            if (recipeFromBase is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipe.Id.ToString());
            }

            _recipeRepository.Change(recipe);
        }
        catch (Exception exception)
        {
            LogError("Update", exception);
            throw;
        }
    }

    public void Delete(RecipeId recipeId)
    {
        try
        {
            // проверка на существование этого рецепта в базе
            var recipe = _recipeRepository.FindById(recipeId);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeId.ToString());
            }

            _recipeRepository.Delete(recipe);
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
