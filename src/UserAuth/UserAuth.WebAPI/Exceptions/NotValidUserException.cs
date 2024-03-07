using System.Runtime.Serialization;
using UserAuth.BusinessLogic.Dto;

namespace UserAuth.WebApi.Exceptions
{
    public class NotValidUserException : ExceptionBase
    {
        public NotValidUserException()
        {
            
        }

        public NotValidUserException(string? message) : base(message)
        {
        }

        public NotValidUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotValidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
