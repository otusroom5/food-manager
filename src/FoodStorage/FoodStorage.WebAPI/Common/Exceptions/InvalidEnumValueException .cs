namespace FoodStorage.WebApi.Common.Exceptions;

public class InvalidEnumValueException : WebApiException
{
    public InvalidEnumValueException(string fieldName, string fieldValue, string enumName)
        : base($"Невозможно преобразовать значение поля {fieldName}: '{fieldValue}' в тип '{enumName}'")
    { }
}