namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель создания единицы продукта
/// </summary>
public sealed record ProductItemCreateRequestModel
{
    /// <summary>
    /// Продукт
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public string UnitId { get; set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; set; }
}

