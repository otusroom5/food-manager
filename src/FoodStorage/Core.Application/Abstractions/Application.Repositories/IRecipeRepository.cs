using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Repositories
{
    public interface IRecipeRepository
    {
        public RecipeId Create(Recipe recipe);
        public Recipe FindById(RecipeId recipeId);
        public IEnumerable<Recipe> GetByProductId(ProductId productId);
        public IEnumerable<Recipe> GetAll();
        public void Change(Recipe recipe);
        public void Delete(Recipe recipe);
    }
}
