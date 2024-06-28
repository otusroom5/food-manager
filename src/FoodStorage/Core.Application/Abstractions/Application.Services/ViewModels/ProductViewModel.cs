namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель продукта
/// </summary>
public sealed record ProductViewModel
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование продукта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип единицы изменерения
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
