namespace FoodStorage.Application.Implementations.Common.Exceptions;

public class EntityNotFoundException : ApplicationLayerException
{
    public EntityNotFoundException(string entity, string paramValue) 
        : base($"Сущность '{entity}' с указанными параметрами: '{paramValue}' не была найдена")
    { }
}
