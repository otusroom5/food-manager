using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
public static class RecipeDtoExtension
{
    public static Recipe ToEntity(this RecipeDto recipeDto) =>
        Recipe.CreateNew(RecipeId.FromGuid(recipeDto.Id), RecipeName.FromString(recipeDto.Name), recipeDto.Positions.Select(p => p.ToEntity()));

    public static RecipeDto ToDto(Recipe recipe) =>
        new()
        {
            Id = recipe.Id.ToGuid(),
            Name = recipe.Name.ToString(),
            Positions = recipe.Positions.Select(p => p.ToDto(recipe.Id)).ToList()
        };
}
