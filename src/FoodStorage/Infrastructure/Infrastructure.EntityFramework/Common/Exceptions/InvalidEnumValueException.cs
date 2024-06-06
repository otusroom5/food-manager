namespace FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

public class InvalidEnumValueException : InfrastructureException
{
    public InvalidEnumValueException(string fieldName, string fieldValue, string enumName)
        : base($"Cannot convert field value {fieldName}: '{fieldValue}' to type '{enumName}'")
    { }
}
