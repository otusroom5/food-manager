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
    /// Тип единиц изменерения
    /// </summary>
    public string UnitType { get; set; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public double MinAmountPerDay { get; set; }

    /// <summary>
    /// Срок годности в днях
    /// </summary>
    public int BestBeforeDate { get; set; }
}
