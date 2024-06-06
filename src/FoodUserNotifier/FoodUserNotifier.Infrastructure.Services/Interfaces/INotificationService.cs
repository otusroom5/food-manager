namespace FoodUserNotifier.Infrastructure.Services.Interfaces;

public interface INotificationService : IDisposable
{
    void StartListen();
}
