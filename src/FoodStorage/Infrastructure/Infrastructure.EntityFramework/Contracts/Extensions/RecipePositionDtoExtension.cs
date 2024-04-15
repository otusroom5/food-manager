using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class RecipePositionDtoExtension
{
    public static RecipePosition ToEntity(this RecipePositionDto recipePositionDto) =>
        RecipePosition.CreateNew(ProductId.FromGuid(recipePositionDto.ProductId), recipePositionDto.ProductCount);

    public static RecipePositionDto ToDto(this RecipePosition recipePosition, RecipeId recipeId) =>
        new()
        {
            RecipeId = recipeId.ToGuid(),
            ProductId = recipePosition.ProductId.ToGuid(),
            ProductCount = recipePosition.ProductCount
        };
}
