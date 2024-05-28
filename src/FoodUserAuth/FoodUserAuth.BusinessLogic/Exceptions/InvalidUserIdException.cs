using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
public class InvalidUserIdException : DomainBaseException
{
    public InvalidUserIdException(): this ("Invalid user id")
    {
    }

    public InvalidUserIdException(string message) : base(message)
    {
    }

    public InvalidUserIdException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidUserIdException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
