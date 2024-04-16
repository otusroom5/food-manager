using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.WebApi.Models.Extensions;

public static class RecipeModelExtension
{
    public static Recipe ToEntity(this RecipeModel recipeModel)
    {
        // При создании в рецепте может не быть позиций
        IEnumerable<RecipePosition> positions = recipeModel.Positions is null ? null : recipeModel.Positions.Select(p => p.ToEntity());

        return Recipe.CreateNew(RecipeId.FromGuid(recipeModel.Id), RecipeName.FromString(recipeModel.Name), positions);
    }

    public static RecipeModel ToModel(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id.ToGuid(),
            Name = recipe.Name.ToString(),
            Positions = recipe.Positions.Select(p => p.ToModel()).ToList()
        };
}
