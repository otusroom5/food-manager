﻿using System.Runtime.Serialization;
using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.WebApi.Exceptions
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
