using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Entities;

namespace FoodUserNotifier.Infrastructure.Telegram.Services.Interfaces;

public interface ITelegramBackgroundService
{
    Task StartListenAsync();
    Task SendMessageAsync(Recepient recepient, string messageText, 
        TelegramSession session, CancellationToken cancellationToken);
    Task SendMessageAsync(Recepient recepient, string messageText, TelegramSession session,
        Stream attachmentStream, CancellationToken cancellationToken);
}
