using System.Runtime.Serialization;

namespace FoodUserAuth.DataAccess.Exceptions
{
    [Serializable]
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException()
        {
        }

        public NotFoundUserException(string message) : base(message)
        {
        }

        public NotFoundUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
