using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт взят из холодильника"
/// </summary>
public class ReducedProductItemDomainEvent : BaseDomainEvent
{
    /// <summary>
    /// Единица продукта из холодильника
    /// </summary>
    public ProductItem ProductItem { get; }

    /// <summary>
    /// Количество взятого продукта
    /// </summary>
    public double ProductCount { get; }

    public ReducedProductItemDomainEvent(ProductItem productItem, double productCount, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        ProductItem = productItem;
        ProductCount = productCount;
    }
}
