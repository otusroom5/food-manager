using FoodStorage.Domain.Entities.Common;
using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.UnitEntity;

/// <summary>
/// Идентификатор единицы измерения
/// </summary>
public record UnitId
{
    private readonly string _value;

    private UnitId(string value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static UnitId FromString(string unitName)
    {
        if (string.IsNullOrWhiteSpace(unitName))
        {
            throw new InvalidArgumentValueException("Empty value passed", nameof(UnitId));
        }

        if (unitName.Length is < 2 or > 4)
        {
            throw new InvalidArgumentValueException("Incorrect value passed", nameof(UnitId));
        }

        return new UnitId(unitName);
    }
}
