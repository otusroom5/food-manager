using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
internal class NotValidUserException : DomainBaseException
{
    public NotValidUserException()
    {
    }

    public NotValidUserException(string message) : base(message)
    {
    }

    public NotValidUserException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotValidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
