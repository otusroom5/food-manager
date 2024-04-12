using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.ProductEntity;

namespace Application.Repositories.Interface
{
    public interface IRecipeRepository
    {
      public RecipeId Create(Recipe value);
      public ProductHistory FindById(RecipeId value); 
      public IEnumerable<Recipe> GetByProductId(ProductId value); 
      public IEnumerable<Recipe> GetAll();
      public void Change(Recipe value);
      public void Delete(Recipe value);
    }
}
