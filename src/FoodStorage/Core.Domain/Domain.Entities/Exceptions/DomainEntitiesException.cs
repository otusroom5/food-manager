namespace Domain.Entities.Exceptions;

public class DomainEntitiesException : Exception
{
    public DomainEntitiesException(string message) : base(message) { }

    public DomainEntitiesException(string message, string className, string propertyName) 
        : base($"{message}. Entity = '{className}', property = '{propertyName}'") 
    { }
}
