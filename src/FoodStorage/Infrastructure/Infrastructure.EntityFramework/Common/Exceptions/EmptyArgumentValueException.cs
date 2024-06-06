namespace FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

public class EmptyArgumentValueException : InfrastructureException
{
    public EmptyArgumentValueException(string argumentName) : base($"Argument {argumentName} not specified")
    { }
}
