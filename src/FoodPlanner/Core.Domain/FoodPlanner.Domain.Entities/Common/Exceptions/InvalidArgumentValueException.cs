namespace FoodPlanner.Domain.Entities.Common.Exceptions;

public class InvalidArgumentValueException: DomainEntitiesException
{
    public InvalidArgumentValueException(string message, string argumentName)
     : base($"Incorrect argument '{argumentName}' : {message}")
    { }
}
