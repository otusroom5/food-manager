using System.ComponentModel;

namespace FoodStorage.WebApi.Models.ProductItemModels;

/// <summary>
/// Модель единицы продукта (в холодильнике)
/// </summary>
public class ProductItemModel
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public Guid Id { get; set; }

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

    [DefaultValue(null)]
    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}