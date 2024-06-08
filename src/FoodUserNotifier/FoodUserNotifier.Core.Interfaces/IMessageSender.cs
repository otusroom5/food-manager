using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Core.Domain.Interfaces;

public interface IMessageSender
{
    Task SendAsync(Message message, Report report);
}
