using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Core.Domain.Interfaces;
public interface IMessageDispatcher
{
    Task SendAllAsync(Notification notification);
}