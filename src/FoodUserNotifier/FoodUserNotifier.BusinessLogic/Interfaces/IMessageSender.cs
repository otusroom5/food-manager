using FoodUserNotifier.DataAccess.Entities;

namespace FoodUserNotifier.BusinessLogic.Interfaces;

public interface IMessageSender
{
    Task SendAsync(IEnumerable<Recepient> recepients, string message);
}
