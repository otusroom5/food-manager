using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class RecipeModelExtension
{
    public static RecipeViewModel ToViewModel(this Recipe recipe, List<RecipePositionViewModel> recipePositions = null) =>
        new()
        {
            Id = recipe.Id.ToGuid(),
            Name = recipe.Name.ToString(),
            Positions = recipePositions
        };

    public static Recipe ToEntity(this RecipeCreateRequestModel recipeModel)
    {
        // При создании в рецепте может не быть позиций
        IEnumerable<RecipePosition> positions = recipeModel.Positions is null ? null : recipeModel.Positions.Select(p => p.ToEntity());

        return Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString(recipeModel.Name), positions);
    }

    public static Recipe ToEntity(this RecipeUpdateRequestModel recipeModel)
    {
        // При редактировании в рецепте может не быть позиций
        IEnumerable<RecipePosition> positions = recipeModel.Positions is null ? null : recipeModel.Positions.Select(p => p.ToEntity());

        return Recipe.CreateNew(RecipeId.FromGuid(recipeModel.Id), RecipeName.FromString(recipeModel.Name), positions);
    }
}
