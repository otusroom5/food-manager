namespace FoodStorage.Domain.Entities.Common.Exceptions;

public class InvalidArgumentValueException : DomainEntitiesException
{
    public InvalidArgumentValueException(string message, string argumentName) 
        : base($"Некорректное значение аргумента '{argumentName}' : {message}") 
    { }
}
