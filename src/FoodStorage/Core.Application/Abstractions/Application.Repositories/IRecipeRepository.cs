using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Repositories;

public interface IRecipeRepository
{
    public void Create(Recipe recipe);
    public Recipe FindById(RecipeId recipeId);
    public Recipe FindByName(RecipeName recipeName);
    public IEnumerable<Recipe> GetByProductId(ProductId productId);
    public IEnumerable<Recipe> GetAll();
    public void Change(Recipe recipe);
    public void Delete(Recipe recipe);
}