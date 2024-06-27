using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Событие "Продукт взят из холодильника"
/// </summary>
public class ReducedProductItemDomainEvent : BaseDomainEvent
{
    // Информация о Единице продукта из холодильника
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; }

    /// <summary>
    /// Количество взятого продукта
    /// </summary>
    public double ProductCount { get; }

    public ReducedProductItemDomainEvent(ProductItem productItem, double productCount, UserId reducedBy, DateTime reducedAt) : base(reducedBy, reducedAt)
    {
        Id = productItem.Id.ToGuid();
        ProductId = productItem.ProductId.ToGuid();
        CreatingDate = productItem.CreatingDate;
        ExpiryDate = productItem.ExpiryDate;
        ProductCount = productCount;
    }
}
