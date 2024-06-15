using System.Runtime.Serialization;

namespace FoodUserNotifier.Infrastructure.Sources.Exceptions;

[Serializable]
public class InvalidServiceResponseException : System.Exception
{
    public InvalidServiceResponseException() : this("Invalid Response from service")
    {
    }

    public InvalidServiceResponseException(string message) : base(message)
    {
    }

    public InvalidServiceResponseException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidServiceResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
