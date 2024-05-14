using System.Runtime.Serialization;

namespace FoodManager.WebUI.Exceptions;

[Serializable]
public class InvalidAccountException : ExceptionBase
{
    public InvalidAccountException()
    {
    }

    public InvalidAccountException(string message) : base(message)
    {
    }

    public InvalidAccountException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
