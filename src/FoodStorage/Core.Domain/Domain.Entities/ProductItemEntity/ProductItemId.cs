using Domain.Entities.Exceptions;

namespace Domain.Entities.ProductItemEntity;

/// <summary>
/// Идентификатор единицы продукта
/// </summary>
public record ProductItemId
{
    private readonly Guid _value;

    private ProductItemId(Guid productItemId)
    {
        if (productItemId != Guid.Empty)
        {
            throw new DomainEntitiesException($"Передано некорректное значение {nameof(ProductItemId)}");
        }

        _value = productItemId;
    }

    public Guid ToGuid() => _value;
    public static ProductItemId FromGuid(Guid value) => new ProductItemId(value);

    public static ProductItemId CreateNew() => new ProductItemId(Guid.NewGuid());
}
