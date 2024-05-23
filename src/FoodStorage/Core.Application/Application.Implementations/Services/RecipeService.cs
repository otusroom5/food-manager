using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Implementations.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IProductRepository _productRepository;

    public RecipeService(IRecipeRepository recipeRepository, IProductRepository productRepository)
    {
        _recipeRepository = recipeRepository;
        _productRepository = productRepository;
    }

    public RecipeId Create(Recipe recipe)
    {
        // проверка на существование рецепта с таким же именем
        var recipeWithSameName = _recipeRepository.FindByName(recipe.Name);
        if (recipeWithSameName is not null)
        {
            throw new ApplicationLayerException($"Уже существует рецепт с таким наименованием: {recipe.Name}");
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

    public Recipe GetById(RecipeId recipeId)
    {
        Recipe recipe = _recipeRepository.FindById(recipeId);

        if (recipe is null)
        {
            throw new EntityNotFoundException(nameof(Recipe), recipeId.ToString());
        }

        return recipe;
    }

    public Recipe GetByName(RecipeName recipeName)
    {
        Recipe recipe = _recipeRepository.FindByName(recipeName);

        if (recipe is null)
        {
            throw new EntityNotFoundException(nameof(Recipe), recipeName.ToString());
        }

        return recipe;
    }

    public IEnumerable<Recipe> GetByProductId(ProductId productId) => _recipeRepository.GetByProductId(productId);

    public IEnumerable<Recipe> GetAll() => _recipeRepository.GetAll();

    public void Update(Recipe recipe)
    {
        // проверка на существование этого рецепта в базе
        var recipeFromBase = _recipeRepository.FindById(recipe.Id);

        if (recipeFromBase is null)
        {
            throw new EntityNotFoundException(nameof(Recipe), recipe.Id.ToString());
        }

        _recipeRepository.Change(recipe);
    }

    public void Delete(RecipeId recipeId)
    {
        // проверка на существование этого рецепта в базе
        var recipe = _recipeRepository.FindById(recipeId);

        if (recipe is null)
        {
            throw new EntityNotFoundException(nameof(Recipe), recipeId.ToString());
        }

        _recipeRepository.Delete(recipe);
    }
}
