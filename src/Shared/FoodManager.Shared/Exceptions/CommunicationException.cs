using System.Runtime.Serialization;

namespace FoodManager.Shared.Exceptions;

[Serializable]
internal class CommunicationException : System.Exception
{
    public CommunicationException() : this("Communication exception")
    {
    }

    public CommunicationException(string message) : base(message)
    {
    }

    public CommunicationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
