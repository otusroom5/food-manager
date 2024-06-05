namespace FoodStorage.Application.Implementations.Common.Exceptions;

public class InvalidEnumValueException : ApplicationLayerException
{
    public InvalidEnumValueException(string fieldName, string fieldValue, string enumName)
        : base($"Невозможно преобразовать значение поля {fieldName}: '{fieldValue}' в тип '{enumName}'")
    { }
}