using Domain.Entities.Exceptions;

namespace Domain.Entities.ProductEntity;

/// <summary>
/// Продукт
/// </summary>
public record Product
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId Id { get; init; }

    /// <summary>
    /// Наименование продукта
    /// </summary>
    public ProductName Name { get; init; }

    /// <summary>
    /// Единица изменерения
    /// </summary>
    public ProductUnit Unit { get; init; }

    private int _minAmountPerDay;
    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public int MinAmountPerDay 
    { 
        get => _minAmountPerDay; 
        init
        {
            if (value <= 0)
            {
                throw new DomainEntitiesException("Минимальный остаток должен быть положительным числом", nameof(Product), nameof(MinAmountPerDay));
            }
            _minAmountPerDay = value;
        }
    }

    private double _bestBeforeDate;
    /// <summary>
    /// Срок годности в часах
    /// </summary>
    public double BestBeforeDate
    {
        get => _bestBeforeDate;
        init
        {
            if (value <= 0)
            {
                throw new DomainEntitiesException("Срок годности должен быть положительным числом", nameof(Product), nameof(BestBeforeDate));
            }
            _bestBeforeDate = value;
        }
    }

    public Product(Guid id, string name, ProductUnit unit, int minAmountPerDay, double bestBeforeDate)
    {
        Id =  ProductId.FromGuid(id);
        Name = ProductName.FromString(name);
        Unit = unit;
        MinAmountPerDay = minAmountPerDay;
        BestBeforeDate = bestBeforeDate;
    }
}
