using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace FoodUserNotifier.WebApi.Implementations;

public class TelegramMessageSender : IMessageSender
{
    private readonly ILogger _logger;
    private readonly TelegramBotClient _botClient;
    public TelegramMessageSender(IOptions<TelegramClientOptions> options, ILogger<TelegramMessageSender> logger)
    {
        _logger = logger;
        _botClient = new TelegramBotClient(options.Value.AccessToken);
    }
    public Task SendAsync(IEnumerable<Recepient> recepients, string message)
    {
        using var cts = new CancellationTokenSource();

        Parallel.ForEach(recepients, async recepient =>
        {
            await _botClient.SendTextMessageAsync(
                    chatId: recepient.TelegramChatId,
                    text: message,
                    cancellationToken: cts.Token);

            _logger.LogTrace($"Сообщение отправлено");
        });

        return Task.CompletedTask;
    }
}
