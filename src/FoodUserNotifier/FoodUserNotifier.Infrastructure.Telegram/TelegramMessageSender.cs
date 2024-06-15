using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sender.Telegram.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace FoodUserNotifier.Infrastructure.Sender.Telegram;

public sealed class TelegramMessageSender : IMessageSender
{
    public TelegramMessageSender(IOptions<TelegramClientOptions> options,
        ILoggerFactory loggerFactory, IRecepientsSource recepientSource, IUnitOfWork unitOfWork)
    {
        
    }

    public async Task SendAsync(Core.Entities.Message message, DeliveryReport report)
    {
        throw new NotImplementedException();
    }
}
