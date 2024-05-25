using System;
using System.Runtime.Serialization;

namespace FoodUserAuth.WebApi.Exceptions;

[Serializable]
internal class ExceptionBase : System.Exception
{
    public ExceptionBase()
    {
    }

    public ExceptionBase(string? message) : base(message)
    {
    }

    public ExceptionBase(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
