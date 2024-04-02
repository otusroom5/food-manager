using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.ProductItemEntity;

/// <summary>
/// Единица продукта (продукт в холодильнике)
/// </summary>
public record ProductItem
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public ProductItemId Id { get; init; }

    /// <summary>
    /// Продукт
    /// </summary>
    public ProductId ProductId { get; init; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; init; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; init; }

    private ProductItem() { }

    public static ProductItem CreateNew(ProductItemId id, ProductId productId, int amount, DateTime creatingDate, DateTime expiryDate)
    {
        if (amount <= 0)
        {
            throw new InvalidArgumentValueException("Количество единиц продукта должно быть положительным числом", nameof(Amount));
        }

        if (creatingDate >= DateTime.UtcNow)
        {
            throw new InvalidArgumentValueException("Дата изготовления не может быть из будущего", nameof(CreatingDate));
        }

        if (expiryDate <= creatingDate)
        {
            throw new InvalidArgumentValueException("Дата окончания срока годности не может быть раньше чем дата изготовления", nameof(ExpiryDate));
        }

        return new()
        {
            Id = id,
            ProductId = productId,
            Amount = amount,
            CreatingDate = creatingDate,
            ExpiryDate = expiryDate
        };
    }

    /// <summary>
    /// Забрать часть продукта из холодильника
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceAmount(int amount)
    {
        if (amount > Amount)
        {
            throw new DomainEntitiesException("Нельзя забрать продукта больше, чем есть");
        }

        Amount -= amount;

        //TODO не забыть что при изменении сущности нужна запись в историю + проверка других записей этого продукта на остаток в холодильнике
    }
}
