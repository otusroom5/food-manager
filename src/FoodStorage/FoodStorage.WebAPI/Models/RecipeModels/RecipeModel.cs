namespace FoodStorage.WebApi.Models.RecipeModels;

/// <summary>
/// Модель рецепта
/// </summary>
public class RecipeModel
{
    /// <summary>
    /// Идентификатор рецепта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Позиции рецепта (продукт + его количество)
    /// </summary>
    public List<RecipePositionModel> Positions { get; set; } = new List<RecipePositionModel>();
}
