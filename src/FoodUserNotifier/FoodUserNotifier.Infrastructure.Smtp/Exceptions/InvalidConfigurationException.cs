using System.Runtime.Serialization;


namespace FoodUserNotifier.Infrastructure.Smtp.Exceptions;

[Serializable]
public class InvalidConfigurationException : System.Exception
{
    public InvalidConfigurationException()
    {
    }

    public InvalidConfigurationException(string message) : base(message)
    {
    }

    public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
