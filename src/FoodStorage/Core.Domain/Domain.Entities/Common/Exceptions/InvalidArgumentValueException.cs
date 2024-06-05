namespace FoodStorage.Domain.Entities.Common.Exceptions;

public class InvalidArgumentValueException : DomainEntitiesException
{
    public InvalidArgumentValueException(string message, string argumentName) 
        : base($"Invalid argument value '{argumentName}' : {message}") 
    { }
}
