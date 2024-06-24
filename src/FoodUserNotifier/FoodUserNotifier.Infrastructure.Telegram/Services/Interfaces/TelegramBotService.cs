using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Entities;

namespace FoodUserNotifier.Infrastructure.Telegram.Services.Interfaces;

public interface ITelegramBackgroundService
{
    Task StartListenAsync();
    Task SendMessageAsync(Recepient recepient, Message message, 
        TelegramSession session, CancellationToken cancellationToken);
}
