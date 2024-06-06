using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Sender.Telegram.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FoodUserNotifier.Infrastructure.Sender.Telegram;

public class TelegramMessageSender : IMessageSender
{
    private readonly IOptions<TelegramClientOptions> _options;
    private readonly ILogger _logger;
    public TelegramMessageSender(IOptions<TelegramClientOptions> options, ILoggerFactory loggerFactory)
    {
        _options = options;
        _logger = loggerFactory.CreateLogger<TelegramMessageSender>();
    }

    public Task SendAsync(Message message, in Report report)
    {
        throw new NotImplementedException();
    }
}
