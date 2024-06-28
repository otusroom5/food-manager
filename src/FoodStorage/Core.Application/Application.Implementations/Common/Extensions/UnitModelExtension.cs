using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class UnitModelExtension
{
    public static UnitViewModel ToViewModel(this Unit unit) =>
        new()
        {
            Id = unit.Id.ToString(),
            Name = unit.Name.ToString(),
            UnitType = unit.UnitType.ToString(),
            Coefficient = unit.Coefficient
        };

    public static Unit ToEntity(this UnitCreateRequestModel unitModel)
    {
        if (!Enum.TryParse<UnitType>(unitModel.UnitType, true, out var unitType))
        {
            throw new InvalidEnumValueException(nameof(unitModel.UnitType), unitModel.UnitType, nameof(UnitType));
        }

        return Unit.CreateNew(UnitId.FromString(unitModel.Id), UnitName.FromString(unitModel.Name), unitType, unitModel.Coefficient);
    }
}
