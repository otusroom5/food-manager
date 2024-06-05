namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель единицы продукта (в холодильнике)
/// </summary>
public sealed record ProductItemViewModel
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Продукт
    /// </summary>
    public ProductShortViewModel Product { get; init; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public int Amount { get; init; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; init; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; init; }
}
