using System.Runtime.Serialization;

namespace FoodUserAuth.BusinessLogic.Exceptions
{
    [Serializable]
    public class DomainBaseException : Exception
    {
        public DomainBaseException()
        {
        }

        public DomainBaseException(string message) : base(message)
        {
        }

        public DomainBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
