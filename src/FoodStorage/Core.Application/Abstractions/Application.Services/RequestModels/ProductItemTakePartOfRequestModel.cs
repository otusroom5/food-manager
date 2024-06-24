namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель взятия единицы продукта из холодильника
/// </summary>
public sealed record ProductItemTakePartOfRequestModel
{
    /// <summary>
    /// Продукт
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Количество продукта, которое хотят взять
    /// </summary>
    public double Count { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения, в которой забирается продукт
    /// </summary>
    public string UnitId { get; set; }

    /// <summary>
    /// Пользователь, забирающий продукт
    /// </summary>
    public Guid UserId { get; set; }
}
