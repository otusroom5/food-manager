namespace FoodPlanner.BusinessLogic.Exceptions;

public static class GuidException
{
    public static void ValidateOrThrow(this Guid guid, string propertyName)
    {
        if (guid == Guid.Empty)
        {
            throw new InvalidArgumentValueException("Guid is empty", propertyName);
        }
    }
}
