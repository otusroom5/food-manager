namespace FoodStorage.Domain.Entities.Common.Exceptions;

public class DomainEntitiesException : Exception
{
    public DomainEntitiesException(string message) : base(message) { }
}
