using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IRecipeService
{
    public Task<Guid> CreateAsync(RecipeCreateRequestModel recipe);
    public Task<RecipeViewModel> GetByIdAsync(Guid recipeId);
    public Task<RecipeViewModel> GetByNameAsync(string recipeName);
    public Task<List<RecipeViewModel>> GetByProductIdAsync(Guid productId);
    public Task<List<RecipeViewModel>> GetAllAsync();
    public Task UpdateAsync(RecipeUpdateRequestModel recipe);
    public Task DeleteAsync(Guid recipeId);
}
