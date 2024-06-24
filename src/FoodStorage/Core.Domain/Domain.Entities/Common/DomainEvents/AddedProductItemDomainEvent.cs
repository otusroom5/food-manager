using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт добавлен в холодильник"
/// </summary>
public class AddedProductItemDomainEvent : BaseDomainEvent
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId ProductId { get; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public double ProductCount { get; }

    public AddedProductItemDomainEvent(ProductId productId, double productCount, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        ProductId = productId;
        ProductCount = productCount;
    }
}
