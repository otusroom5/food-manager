namespace FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

public class InvalidEnumValueException : InfrastructureException
{
    public InvalidEnumValueException(string fieldName, string fieldValue, string enumName)
        : base($"Невозможно преобразовать значение поля {fieldName}: '{fieldValue}' в тип '{enumName}'")
    { }
}
