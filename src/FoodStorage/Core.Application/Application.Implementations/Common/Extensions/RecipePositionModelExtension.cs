using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class RecipePositionModelExtension
{
    public static RecipePositionViewModel ToViewModel(this RecipePosition recipePosition, Product product) =>
        new()
        {
            Product = product.ToShortViewModel(),
            ProductCount = recipePosition.ProductCount
        };

    public static RecipePosition ToEntity(this RecipePositionRequestModel recipePositionModel) =>
        RecipePosition.CreateNew(ProductId.FromGuid(recipePositionModel.ProductId), recipePositionModel.ProductCount);
}
