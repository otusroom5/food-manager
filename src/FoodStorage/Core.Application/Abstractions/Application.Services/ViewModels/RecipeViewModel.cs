namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель рецепта
/// </summary>
public sealed record RecipeViewModel
{
    /// <summary>
    /// Идентификатор рецепта
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Позиции рецепта (продукт + его количество)
    /// </summary>
    public List<RecipePositionViewModel> Positions { get; init; } = new List<RecipePositionViewModel>();
}
