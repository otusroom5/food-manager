namespace FoodStorage.Domain.Entities.Common.Events;

public sealed class ProductExpiringEventMessage : BaseEventMessage
{
    /// <summary>
    /// Единицы продукта в холодильнике, которые скоро испортятся
    /// </summary>
    public List<ExpiringProduct> ProductItems { get; }

    public ProductExpiringEventMessage(List<ExpiringProduct> productItems, DateTime occuredOn) : base(occuredOn)
    {
        ProductItems = productItems;
    }
}

/// <summary>
/// Модель единицы продукта (в холодильнике)
/// </summary>
public sealed record ExpiringProduct
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Продукт
    /// </summary>
    public string ProductName { get; init; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public double Amount { get; init; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; init; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; init; }
}
