using Domain.Entities.Exceptions;

namespace Domain.Entities.ProductHistoryEntity;

/// <summary>
/// Идентификатор истории продукта
/// </summary>
public record ProductHistoryId
{
    private readonly Guid _value;

    private ProductHistoryId(Guid productHistoryId)
    {
        if (productHistoryId != Guid.Empty)
        {
            throw new DomainEntitiesException($"Передано некорректное значение {nameof(ProductHistoryId)}");
        }

        _value = productHistoryId;
    }

    public Guid ToGuid() => _value;
    public static ProductHistoryId FromGuid(Guid value) => new ProductHistoryId(value);

    public static ProductHistoryId CreateNew() => new ProductHistoryId(Guid.NewGuid());
}
