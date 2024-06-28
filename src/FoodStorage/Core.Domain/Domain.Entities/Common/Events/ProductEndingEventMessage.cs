namespace FoodStorage.Domain.Entities.Common.Events;

public sealed class ProductEndingEventMessage : BaseEventMessage
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public string ProductName { get; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public double MinAmountPerDay { get; }

    /// <summary>
    /// Количество оставшегося продукта
    /// </summary>
    public double Amount { get; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public string Unit { get; }

    public ProductEndingEventMessage(string productName, double minAmountPerDay, double amount, string unit, DateTime occuredOn) 
        : base(occuredOn)
    {
        ProductName = productName;
        MinAmountPerDay = minAmountPerDay;
        Amount = amount;
        Unit = unit;
    }
}
