namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель создания единицы измерения
/// </summary>
public sealed record UnitCreateRequestModel
{
    /// <summary>
    /// Идентификатор (короткое наименование)
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Тип единиц измерения
    /// </summary>
    public string UnitType { get; init; }

    /// <summary>
    /// Коэффициент, на который надо умножить чтобы получить стандарт в этой группе
    /// </summary>
    public double Coefficient { get; init; }
}
