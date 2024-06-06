using System;
using System.Runtime.Serialization;

namespace FoodUserAuth.WebApi.Exceptions;

[Serializable]
internal class InvalidUserRoleException : ExceptionBase
{
    public InvalidUserRoleException() : this("Invalid user role")
    {
    }

    public InvalidUserRoleException(string message) : base(message)
    {
    }

    public InvalidUserRoleException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidUserRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
