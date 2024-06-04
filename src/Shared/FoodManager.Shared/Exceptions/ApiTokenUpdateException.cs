using System.Runtime.Serialization;

namespace FoodManager.Shared.Exceptions;

[Serializable]
internal class ApiTokenUpdateException : System.Exception
{
    public ApiTokenUpdateException() : this("Communication exception")
    {
    }

    public ApiTokenUpdateException(string message) : base(message)
    {
    }

    public ApiTokenUpdateException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ApiTokenUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
