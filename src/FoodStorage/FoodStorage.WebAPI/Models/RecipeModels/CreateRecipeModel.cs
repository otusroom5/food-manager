namespace FoodStorage.WebApi.Models.RecipeModels;

/// <summary>
/// Модель создания рецепта
/// </summary>
public class CreateRecipeModel
{
    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Позиции рецепта (продукт + его количество)
    /// </summary>
    public List<RecipePositionModel> Positions { get; set; } = new List<RecipePositionModel>();
}
