using Domain.Entities.Exceptions;

namespace Domain.Entities.ProductEntity;

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
            throw new DomainEntitiesException($"Передано пустое значение {nameof(ProductName)}");
        }

        if (productName.Length is < 1 or > 250)
        {
            throw new DomainEntitiesException($"Некорректное значение параметра {nameof(ProductName)}: '{productName}'");
        }

        return new ProductName(productName);
    }
}
