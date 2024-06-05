namespace FoodStorage.Application.Implementations.Common.Exceptions;

public class InvalidEnumValueException : ApplicationLayerException
{
    public InvalidEnumValueException(string fieldName, string fieldValue, string enumName)
        : base($"Cannot convert field value {fieldName}: '{fieldValue}' to type '{enumName}'")
    { }
}