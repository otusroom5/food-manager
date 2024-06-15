namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель истории продуктов
/// </summary>
public sealed record ProductHistoryViewModel
{
    /// <summary>
    /// Продукт
    /// </summary>
    public ProductShortViewModel Product { get; init; }

    /// <summary>
    /// Действие с продуктом (статус продукта в истории)
    /// </summary>
    public string State { get; init; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Кто провел действие с продуктом
    /// </summary>
    public Guid CreatedBy { get; init; }

    /// <summary>
    /// Когда было проведено действие с продуктом
    /// </summary>
    public DateTime CreatedAt { get; init; }
}
