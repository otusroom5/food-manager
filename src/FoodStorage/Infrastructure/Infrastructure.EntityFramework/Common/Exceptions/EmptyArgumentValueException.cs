namespace FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

public class EmptyArgumentValueException : InfrastructureException
{
    public EmptyArgumentValueException(string argumentName) : base($"Аргумент {argumentName} не указан")
    { }
}
