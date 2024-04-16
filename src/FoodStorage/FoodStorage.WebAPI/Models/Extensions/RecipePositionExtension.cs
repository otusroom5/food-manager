using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.WebApi.Models.Extensions
{
    public static class RecipePositionExtension
    {
        public static RecipePosition ToEntity(this RecipePositionModel recipePositionModel) =>
            RecipePosition.CreateNew(ProductId.FromGuid(recipePositionModel.ProductId), recipePositionModel.ProductCount);

        public static RecipePositionModel ToModel(this RecipePosition recipePosition) =>
            new()
            {
                ProductId = recipePosition.ProductId.ToGuid(),
                ProductCount = recipePosition.ProductCount
            };
    }
}
