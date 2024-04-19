namespace FoodUserNotifier.BusinessLogic.Interfaces;

public interface IMessageDispatcher
{
    Task SendAllAsync(string message);
}