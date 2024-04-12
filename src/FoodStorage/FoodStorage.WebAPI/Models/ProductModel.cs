namespace FoodStorage.WebApi.Models;

/// <summary>
/// Модель продукта
/// </summary>
public class ProductModel
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
    /// Единица изменерения
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Минимальный остаток на день
    /// </summary>
    public int MinAmountPerDay { get; set; }

    /// <summary>
    /// Срок годности в часах
    /// </summary>
    public double BestBeforeDate { get; set; }
}
