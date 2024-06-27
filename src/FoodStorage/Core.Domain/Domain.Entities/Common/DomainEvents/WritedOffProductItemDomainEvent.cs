using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт списан"
/// </summary>
public class WritedOffProductItemDomainEvent : BaseDomainEvent
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Продукт
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public double Amount { get; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; }

    public WritedOffProductItemDomainEvent(ProductItem productItem, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        Id = productItem.Id.ToGuid();
        ProductId = productItem.ProductId.ToGuid();
        Amount = productItem.Amount;
        CreatingDate = productItem.CreatingDate;
        ExpiryDate = productItem.ExpiryDate;
    }
}