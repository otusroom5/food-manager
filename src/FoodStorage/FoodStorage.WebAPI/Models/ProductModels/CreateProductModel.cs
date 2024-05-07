namespace FoodStorage.WebApi.Models.ProductModels;

/// <summary>
/// Модель создания продукта
/// </summary>
public class CreateProductModel
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
