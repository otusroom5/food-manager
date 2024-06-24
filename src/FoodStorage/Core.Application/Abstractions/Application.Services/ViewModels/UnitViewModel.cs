namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Модель единицы измерения
/// </summary>
public sealed record UnitViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Тип ес
    /// </summary>
    public string UnitType { get; init; }

    /// <summary>
    /// Коэффициент (на что умножить чтобы получить стандарт в группе)
    /// </summary>
    public double Coefficient { get; init; }
}
