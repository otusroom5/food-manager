using Domain.Entities.Exceptions;
using Domain.Entities.ProductEntity;

namespace Domain.Entities.ProductItemEntity;

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

    private DateTime _creatingDate;
    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate
    {
        get => _creatingDate;
        init
        {
            if (value >= DateTime.UtcNow)
            {
                throw new DomainEntitiesException("Дата изготовления не может быть из будущего", nameof(ProductItem), nameof(CreatingDate));
            }
            _creatingDate = value;
        }
    }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; init; }

    public ProductItem(Guid id, Guid productId, int amount, DateTime creatingDate, DateTime expiryDate)
    {
        Id = ProductItemId.FromGuid(id);
        ProductId = ProductId.FromGuid(productId);
        Amount = amount;
        CreatingDate = creatingDate;
        ExpiryDate = expiryDate;
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

        //TODO можно подумать над доменными событиями, но это уже усложнение системы. нужно ли оно нам?... или лучше все закладывать в апп-сервисы
        //(для записи в историю + проверки других записей этого продукта на остаток в холодильнике)
    }
}
