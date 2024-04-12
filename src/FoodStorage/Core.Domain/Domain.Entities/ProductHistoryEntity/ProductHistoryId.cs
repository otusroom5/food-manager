using FoodStorage.Domain.Entities.Common;

namespace FoodStorage.Domain.Entities.ProductHistoryEntity;

/// <summary>
/// Идентификатор истории продукта
/// </summary>
public record ProductHistoryId
{
    private readonly Guid _value;

    private ProductHistoryId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static ProductHistoryId FromGuid(Guid value)
    {
        value.ValidateOrThrow(nameof(ProductHistoryId));
        return new ProductHistoryId(value);
    }

    public static ProductHistoryId CreateNew() => new ProductHistoryId(Guid.NewGuid());
}
