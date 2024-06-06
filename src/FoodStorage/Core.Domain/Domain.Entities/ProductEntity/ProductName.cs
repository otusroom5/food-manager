using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.ProductEntity;

/// <summary>
/// Наименование продукта
/// </summary>
public record ProductName
{
    private readonly string _value;

    private ProductName(string value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static ProductName FromString(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new InvalidArgumentValueException("Empty value passed", nameof(ProductName));
        }

        if (productName.Length is < 2 or > 100)
        {
            throw new InvalidArgumentValueException("Incorrect value passed", nameof(ProductName));
        }

        return new ProductName(productName);
    }
}
