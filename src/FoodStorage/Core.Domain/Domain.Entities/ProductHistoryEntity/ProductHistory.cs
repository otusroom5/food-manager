using Domain.Entities.Exceptions;
using Domain.Entities.ProductEntity;

namespace Domain.Entities.ProductHistoryEntity;

/// <summary>
/// История хранения продуктов. Продуктооборот
/// </summary>
public record ProductHistory
{
    /// <summary>
    /// Идентификатор истории продукта
    /// </summary>
    public ProductHistoryId Id { get; init; }

    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId ProductId { get; init; }

    /// <summary>
    /// Действие с продуктом (статус продукта в истории)
    /// </summary>
    public ProductState State { get; init; }

    private int _count;
    /// <summary>
    /// Количество продукта
    /// </summary>
    public int Count 
    { 
        get => _count; 
        init
        {
            if (value <= 0)
            {
                throw new DomainEntitiesException("Количество продукта должно быть положительным числом", nameof(ProductHistory), nameof(Count));
            }
            _count = value;
        }
    }

    /// <summary>
    /// Кто провел действие с продуктом
    /// </summary>
    public UserId CreatedBy { get; init; }

    private DateTime _createdAt;
    /// <summary>
    /// Когда было проведено действие с продуктом
    /// </summary>
    public DateTime CreatedAt 
    { 
        get => _createdAt;
        init
        {
            if (value > DateTime.UtcNow)
            {
                throw new DomainEntitiesException("Дата операции не может быть из будущего", nameof(ProductHistory), nameof(CreatedAt));
            }
            _createdAt = value;
        } 
    }

    public ProductHistory(Guid id, Guid productId, ProductState state, int count, Guid createdBy, DateTime createdAt)
    {
        Id = ProductHistoryId.FromGuid(id);
        ProductId = ProductId.FromGuid(productId);
        State = state;
        Count = count;
        CreatedBy = UserId.FromGuid(createdBy);
        CreatedAt = createdAt;
    }
}
