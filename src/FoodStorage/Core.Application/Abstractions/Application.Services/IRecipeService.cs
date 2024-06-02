using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Services;

public interface IRecipeService
{
    public Task<RecipeId> CreateAsync(Recipe recipe);
    public Task<Recipe> GetByIdAsync(RecipeId recipeId);
    public Task<Recipe> GetByNameAsync(RecipeName recipeName);
    public Task<IEnumerable<Recipe>> GetByProductIdAsync(ProductId productId);
    public Task<IEnumerable<Recipe>> GetAllAsync();
    public Task UpdateAsync(Recipe recipe);
    public Task DeleteAsync(RecipeId recipeId);
}
