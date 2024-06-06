namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель создания продукта
/// </summary>
public sealed record ProductCreateRequestModel
{
    /// <summary>
    /// Наименование продукта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Единица изменерения
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public int MinAmountPerDay { get; set; }

    /// <summary>
    /// Срок годности в днях
    /// </summary>
    public double BestBeforeDate { get; set; }
}
