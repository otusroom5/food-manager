using FoodUserNotifier.BusinessLogic.Contracts;


namespace FoodUserNotifier.BusinessLogic.Interfaces;

public interface IMessageSender
{
    Task SendAsync(IEnumerable<RecepientDTO> recepients, string message);
}
