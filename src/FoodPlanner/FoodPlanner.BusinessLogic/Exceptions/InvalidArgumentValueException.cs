namespace FoodPlanner.BusinessLogic.Exceptions;

public class InvalidArgumentValueException : BaseException
{
    public InvalidArgumentValueException(string message, string argumentName)
     : base($"Incorrect argument '{argumentName}' : {message}")
    { }
}