using FoodStorage.Domain.Entities.Common;

namespace FoodStorage.Domain.Entities.ProductEntity;

/// <summary>
/// Идентификатор продукта
/// </summary>
public record ProductId
{
    private readonly Guid _value;

    private ProductId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static ProductId FromGuid(Guid value)
    {
        value.ValidateOrThrow(nameof(ProductId));
        return new ProductId(value);
    }

    public static ProductId CreateNew() => new ProductId(Guid.NewGuid());
}
