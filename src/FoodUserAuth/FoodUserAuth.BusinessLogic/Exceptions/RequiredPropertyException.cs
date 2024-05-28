using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions;

[Serializable]
public sealed class RequiredPropertyException : DomainBaseException
{
    public RequiredPropertyException()
    {
    }

    public RequiredPropertyException(string message) : base(message)
    {
    }

    public RequiredPropertyException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected RequiredPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
