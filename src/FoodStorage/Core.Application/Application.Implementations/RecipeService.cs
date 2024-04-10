using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Implementations;

public class RecipeService : IRecipeService
{
    public RecipeId Create(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public void Delete(RecipeId recipeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Recipe> GetAll()
    {
        throw new NotImplementedException();
    }

    public Recipe GetById(RecipeId recipeId)
    {
        throw new NotImplementedException();
    }

    public Recipe GetByName(RecipeName recipeName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Recipe> GetByProductId(ProductId productId)
    {
        throw new NotImplementedException();
    }

    public void Update(Recipe recipe)
    {
        throw new NotImplementedException();
    }
}
