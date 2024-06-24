namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель позиции рецепта
/// </summary>
public sealed record RecipePositionViewModel
{
    /// <summary>
    /// Модель продукта
    /// </summary>
    public ProductShortViewModel Product { get; init; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public int ProductCount { get; init; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public string Unit { get; set; }
}