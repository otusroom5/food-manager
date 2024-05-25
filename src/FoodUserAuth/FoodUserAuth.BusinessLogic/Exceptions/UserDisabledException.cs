using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
internal class UserDisabledException : DomainBaseException
{
    public UserDisabledException() : this("User is disabled")
    {
    }

    public UserDisabledException(string message) : base(message)
    {
    }

    public UserDisabledException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
