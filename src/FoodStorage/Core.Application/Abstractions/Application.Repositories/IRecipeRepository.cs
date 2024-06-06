using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Repositories;

public interface IRecipeRepository
{
    public Task CreateAsync(Recipe recipe);
    public Task<Recipe> FindByIdAsync(RecipeId recipeId);
    public Task<Recipe> FindByNameAsync(RecipeName recipeName);
    public Task<IEnumerable<Recipe>> GetByProductIdAsync(ProductId productId);
    public Task<IEnumerable<Recipe>> GetAllAsync();
    public Task ChangeAsync(Recipe recipe);
    public Task DeleteAsync(Recipe recipe);
}