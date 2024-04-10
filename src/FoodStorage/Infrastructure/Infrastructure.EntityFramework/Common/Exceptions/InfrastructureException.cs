namespace FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message) { }
}
