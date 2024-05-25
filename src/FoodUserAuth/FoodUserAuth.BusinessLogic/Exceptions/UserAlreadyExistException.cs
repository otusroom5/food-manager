using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
internal class UserAlreadyExistException : DomainBaseException
{
    public UserAlreadyExistException() : this("User already exist")
    {
    }

    public UserAlreadyExistException(string message) : base(message)
    {
    }

    public UserAlreadyExistException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
