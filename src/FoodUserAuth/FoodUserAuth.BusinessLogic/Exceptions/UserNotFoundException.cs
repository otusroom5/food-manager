using FoodUserAuth.DataAccess.Entities;
using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
internal class UserNotFoundException : DomainBaseException
{
    public UserNotFoundException() : this($"User is not found")
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

