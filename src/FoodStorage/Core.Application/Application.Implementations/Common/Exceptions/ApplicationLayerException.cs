namespace FoodStorage.Application.Implementations.Common.Exceptions;

public class ApplicationLayerException : Exception
{
    public ApplicationLayerException(string message) : base(message) { }
}