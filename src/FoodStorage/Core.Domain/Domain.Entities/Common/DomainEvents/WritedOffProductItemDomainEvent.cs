using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт списан"
/// </summary>
public class WritedOffProductItemDomainEvent : BaseDomainEvent
{
    /// <summary>
    /// Единица продукта из холодильника
    /// </summary>
    public ProductItem ProductItem { get; }

    public WritedOffProductItemDomainEvent(ProductItem productItem, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        ProductItem = productItem;
    }
}