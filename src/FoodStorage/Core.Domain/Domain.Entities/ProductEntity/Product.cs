using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.ProductEntity;

/// <summary>
/// Продукт
/// </summary>
public record Product
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId Id { get; init; }

    /// <summary>
    /// Наименование продукта
    /// </summary>
    public ProductName Name { get; init; }

    /// <summary>
    /// Единица изменерения
    /// </summary>
    public ProductUnit Unit { get; init; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public int MinAmountPerDay { get; init; }

    /// <summary>
    /// Срок годности в днях
    /// </summary>
    public double BestBeforeDate { get; init; }

    private Product() { }

    public static Product CreateNew(ProductId id, ProductName name, ProductUnit unit, int minAmountPerDay, double bestBeforeDate)
    {
        if (minAmountPerDay <= 0)
        {
            throw new InvalidArgumentValueException("The minimum balance must be a positive number", nameof(MinAmountPerDay));
        }

        if (bestBeforeDate <= 0)
        {
            throw new InvalidArgumentValueException("Expiration date must be a positive number", nameof(BestBeforeDate));
        }

        return new()
        {
            Id = id,
            Name = name,
            Unit = unit,
            MinAmountPerDay = minAmountPerDay,
            BestBeforeDate = bestBeforeDate
        };
    }
}
