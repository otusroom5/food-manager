using Domain.Entities.Exceptions;

namespace Domain.Entities.ProductEntity;

/// <summary>
/// Идентификатор продукта
/// </summary>
public record ProductId
{
    private readonly Guid _value;

    private ProductId(Guid productId)
    {
        if (productId != Guid.Empty)
        {
            throw new DomainEntitiesException($"Передано некорректное значение {nameof(ProductId)}");
        }

        _value = productId;
    }

    public Guid ToGuid() => _value;
    public static ProductId FromGuid(Guid value) => new ProductId(value);

    public static ProductId CreateNew() => new ProductId(Guid.NewGuid());
}
