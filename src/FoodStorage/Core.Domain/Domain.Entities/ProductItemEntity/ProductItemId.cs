using FoodStorage.Domain.Entities.Common;

namespace FoodStorage.Domain.Entities.ProductItemEntity;

/// <summary>
/// Идентификатор единицы продукта
/// </summary>
public record ProductItemId
{
    private readonly Guid _value;

    private ProductItemId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static ProductItemId FromGuid(Guid value)
    {
        value.ValidateOrThrow(nameof(ProductItemId));
        return new ProductItemId(value);
    }

    public static ProductItemId CreateNew() => new ProductItemId(Guid.NewGuid());
}
