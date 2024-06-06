using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.Common;

public static class GuidExtension
{
    public static void ValidateOrThrow(this Guid guid, string propertyName)
    {
        if (guid == Guid.Empty)
        {
            throw new InvalidArgumentValueException("Empty Guid passed", propertyName);
        }
    }
}
