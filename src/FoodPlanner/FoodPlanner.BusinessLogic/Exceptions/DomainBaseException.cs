namespace FoodPlanner.BusinessLogic.Exceptions;
public class DomainBaseException : Exception
{
    public DomainBaseException(string message) : base(message) { }
}
