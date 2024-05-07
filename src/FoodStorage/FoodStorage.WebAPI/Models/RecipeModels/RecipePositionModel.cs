namespace FoodStorage.WebApi.Models.RecipeModels;

/// <summary>
/// Модель позиции рецепта
/// </summary>
public class RecipePositionModel
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid ProductId { get; set; }
    /// <summary>
    /// Количество продукта
    /// </summary>
    public int ProductCount { get; set; }
}
