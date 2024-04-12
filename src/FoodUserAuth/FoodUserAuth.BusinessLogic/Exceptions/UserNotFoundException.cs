using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions
{
    [Serializable]
    public class UserNotFoundException : DomainBaseException
    {
        public UserNotFoundException()
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
}
