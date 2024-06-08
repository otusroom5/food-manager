using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Sender.Telegram.Options;
using FoodUserNotifier.Infrastructure.Telegram.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Telegram.Bot;

namespace FoodUserNotifier.Infrastructure.Sender.Telegram;

public sealed class TelegramMessageSender : IMessageSender
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TelegramBotClient _botClient;
    private readonly IOptions<TelegramClientOptions> _options;
    private readonly ILogger _logger;

    public TelegramMessageSender(IOptions<TelegramClientOptions> options, 
        ILoggerFactory loggerFactory, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _options = options;
        _logger = loggerFactory.CreateLogger<TelegramMessageSender>();
        
        if (string.IsNullOrWhiteSpace(options.Value.AccessToken))
        {
            throw new InvalidConfigurationException("Telegram access token is not defined");
        }

        _botClient = new TelegramBotClient(options.Value.AccessToken);
    }

    public Task SendAsync(Message message, Report report)
    {
        using var cts = new CancellationTokenSource();

        ITelegramSessionsRepository telegramSessionsRepository = _unitOfWork.GetTelegramSessionsRepository();

        var exceptions = new ConcurrentQueue<Exception>();
        
        Parallel.ForEach(message.Recepients, async recepient =>
        {
            try
            {
                TelegramSession session = await telegramSessionsRepository.GetSessionByRecepientIdAsync(recepient.Id);

                await _botClient.SendTextMessageAsync(
                        chatId: session.ChatId,
                        text: message.MessageText,
                        cancellationToken: cts.Token);

                _logger.LogTrace("Message was send successfully");
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                exceptions.Enqueue(ex);
            }
        });

        if (!exceptions.IsEmpty)
        {
            throw new AggregateException($"Aggregate Exception (NotificationId: {report.NotificationId})", exceptions);
        }

        return Task.CompletedTask;
    }
}
