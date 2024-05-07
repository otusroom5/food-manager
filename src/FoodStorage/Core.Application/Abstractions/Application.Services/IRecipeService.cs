using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Services;

public interface IRecipeService
{
    public RecipeId Create(Recipe recipe);
    public Recipe GetById(RecipeId recipeId);
    public Recipe GetByName(RecipeName recipeName);
    public IEnumerable<Recipe> GetByProductId(ProductId productId);
    public IEnumerable<Recipe> GetAll();
    public void Update(Recipe recipe);
    public void Delete(RecipeId recipeId);
}
