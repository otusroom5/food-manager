using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.UnitEntity;

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
    /// Тип единиц изменерения
    /// </summary>
    public UnitType UnitType { get; init; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public double MinAmountPerDay { get; init; }

    /// <summary>
    /// Срок годности в днях
    /// </summary>
    public int BestBeforeDate { get; init; }

    private Product() { }

    public static Product CreateNew(ProductId id, ProductName name, UnitType unitType, double minAmountPerDay, int bestBeforeDate)
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
            UnitType = unitType,
            MinAmountPerDay = minAmountPerDay,
            BestBeforeDate = bestBeforeDate
        };
    }
}
