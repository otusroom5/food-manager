using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.UnitEntity;

/// <summary>
/// Наименование единицы измерения
/// </summary>
public record UnitName
{
    private readonly string _value;

    private UnitName(string value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static UnitName FromString(string unitName)
    {
        if (string.IsNullOrWhiteSpace(unitName))
        {
            throw new InvalidArgumentValueException("Empty value passed", nameof(UnitName));
        }

        if (unitName.Length is < 2 or > 100)
        {
            throw new InvalidArgumentValueException("Incorrect value passed", nameof(UnitName));
        }

        return new UnitName(unitName);
    }
}