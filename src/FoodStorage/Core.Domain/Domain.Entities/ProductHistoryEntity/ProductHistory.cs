using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.ProductHistoryEntity;

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

    /// <summary>
    /// Количество продукта
    /// </summary>
    public double Count { get; init; }

    /// <summary>
    /// Кто провел действие с продуктом
    /// </summary>
    public UserId CreatedBy { get; init; }

    /// <summary>
    /// Когда было проведено действие с продуктом
    /// </summary>
    public DateTime CreatedAt { get; init; }

    private ProductHistory() { }

    public static ProductHistory CreateNew(ProductHistoryId id, ProductId productId, ProductState state, double count, UserId createdBy, DateTime createdAt)
    {
        if (count <= 0)
        {
            throw new InvalidArgumentValueException("Product quantity must be a positive number", nameof(Count));
        }

        if (createdAt > DateTime.UtcNow)
        {
            throw new InvalidArgumentValueException("Created date cannot be from the future", nameof(CreatedAt));
        }

        return new()
        {
            Id = id,
            ProductId = productId,
            State = state,
            Count = count,
            CreatedBy = createdBy,
            CreatedAt = createdAt
        };
    }
}
