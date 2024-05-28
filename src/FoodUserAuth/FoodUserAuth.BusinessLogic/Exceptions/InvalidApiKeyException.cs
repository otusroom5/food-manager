using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
public sealed class InvalidApiKeyException : DomainBaseException
{
    public InvalidApiKeyException() : this("Invalid Api key")
    {
    }

    public InvalidApiKeyException(string message) : base(message)
    {
    }

    public InvalidApiKeyException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidApiKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

