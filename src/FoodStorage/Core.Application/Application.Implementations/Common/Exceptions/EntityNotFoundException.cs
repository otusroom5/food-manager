namespace FoodStorage.Application.Implementations.Common.Exceptions;

public class EntityNotFoundException : ApplicationLayerException
{
    public EntityNotFoundException(string entity, string paramValue) 
        : base($"Entity '{entity}' with parameters: '{paramValue}' was not found")
    { }
}
