namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель редактирования рецепта
/// </summary>
public sealed record RecipeUpdateRequestModel
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
    public List<RecipePositionRequestModel> Positions { get; set; } = new();
}
