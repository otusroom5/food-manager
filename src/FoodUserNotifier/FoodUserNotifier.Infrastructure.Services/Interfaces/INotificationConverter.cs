using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Infrastructure.Services.Interfaces;

public interface INotificationConverter
{
    Notification Convert(string message);
}
