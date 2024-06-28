using FoodStorage.Domain.Entities.UnitEntity;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class UnitDtoExtension
{
    public static Unit ToEntity(this UnitDto unitDto)
    {
        if (!Enum.TryParse<UnitType>(unitDto.UnitType, out var unitType))
        {
            throw new InvalidEnumValueException(nameof(unitDto.UnitType), unitDto.UnitType, nameof(UnitType));
        }

        return Unit.CreateNew(UnitId.FromString(unitDto.Id), UnitName.FromString(unitDto.Name), unitType, unitDto.Coefficient);
    }

    public static UnitDto ToDto(this Unit unit) =>
        new()
        {
            Id = unit.Id.ToString(),
            Name = unit.Name.ToString(),
            UnitType = unit.UnitType.ToString(),
            Coefficient = unit.Coefficient
        };
}
