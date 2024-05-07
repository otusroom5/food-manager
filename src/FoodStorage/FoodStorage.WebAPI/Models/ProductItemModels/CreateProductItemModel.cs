namespace FoodStorage.WebApi.Models.ProductItemModels;

/// <summary>
/// Модель создания единицы продукта
/// </summary>
public class CreateProductItemModel
{
    /// <summary>
    /// Продукт
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; set; }
}
