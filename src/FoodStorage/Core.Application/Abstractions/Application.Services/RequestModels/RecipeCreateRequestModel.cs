namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель создания рецепта
/// </summary>
public sealed record RecipeCreateRequestModel
{
    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Позиции рецепта (продукт + его количество)
    /// </summary>
    public List<RecipePositionRequestModel> Positions { get; set; } = new();
}
