using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт взят из холодильника"
/// </summary>
public class ReducedProductItemDomainEvent : BaseDomainEvent
{
    /// <summary>
    /// Идентификатор уменьшенного продукта
    /// </summary>
    public ProductId ProductId { get; }

    /// <summary>
    /// Количество взятого продукта
    /// </summary>
    public int ProductCount { get; }

    public ReducedProductItemDomainEvent(ProductId productId, int productCount, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        ProductId = productId;
        ProductCount = productCount;
    }
}
