namespace FoodStorage.WebApi.Common.Exceptions;

public class WebApiException : Exception
{
    public WebApiException(string message) : base(message) { }
}