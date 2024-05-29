namespace FoodPlanner.Domain.Entities.Common.Exceptions;

public static class GuidExtension
{
    public static void ValidateOrThrow(this Guid guid, string propertyName)
    {
        if (guid == Guid.Empty)
        {
            throw new InvalidArgumentValueException("Guid is empty", propertyName);
        }
    }
}
