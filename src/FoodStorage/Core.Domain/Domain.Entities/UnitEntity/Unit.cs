using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.UnitEntity;

/// <summary>
/// Единица измерения продукта
/// </summary>
public record Unit
{
    /// <summary>
    /// Идентификатор в виде короткого названия
    /// </summary>
    public UnitId Id { get; init; }
    /// <summary>
    /// Полное наименование
    /// </summary>
    public UnitName Name { get; init; }
    /// <summary>
    /// Тип единиц измерения
    /// </summary>
    public UnitTypeE UnitType { get; init; }
    /// <summary>
    /// Коэффициент, на который нужно умножить чтобы получить стандартное значение в этой группе измерения
    /// </summary>
    public double Coefficient { get; init; }
    /// <summary>
    /// Является ли эта единица стандартом в группе
    /// </summary>
    public bool IsMain => Coefficient == 1;

    private Unit() { }

    public static Unit CreateNew(UnitId id, UnitName name, UnitTypeE unitType, double coefficient)
    {
        if (coefficient <= 0)
        {
            throw new InvalidArgumentValueException("Proportion in unit must be a positive number", nameof(Coefficient));
        }

        return new()
        {
            Id = id,
            Name = name,
            UnitType = unitType,
            Coefficient = coefficient
        };
    }

    public double ConvertToMain(double value) => value * Coefficient;
}
