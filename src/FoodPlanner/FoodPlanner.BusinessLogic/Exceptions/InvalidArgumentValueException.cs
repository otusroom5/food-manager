namespace FoodPlanner.BusinessLogic.Exceptions;

public class InvalidArgumentValueException : DomainBaseException
{
    public InvalidArgumentValueException(string message, string argumentName)
     : base($"Incorrect argument '{argumentName}' : {message}")
    { }
}