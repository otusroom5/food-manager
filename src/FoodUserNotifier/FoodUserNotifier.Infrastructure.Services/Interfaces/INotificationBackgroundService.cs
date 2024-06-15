using FoodUserNotifier.Core.Interfaces;

namespace FoodUserNotifier.Infrastructure.Services.Interfaces;

public interface INotificationBackgroundService
{
    void StartListen();
}
