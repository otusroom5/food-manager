using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Entities;
using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sender.Telegram.Options;
using FoodUserNotifier.Infrastructure.Sources.Interfaces;
using FoodUserNotifier.Infrastructure.Telegram.Exceptions;
using FoodUserNotifier.Infrastructure.Telegram.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FoodUserNotifier.Infrastructure.Telegram.Services.Implementations;

public sealed class TelegramBackgroundService : BackgroundService, IUpdateHandler, ITelegramBackgroundService
{
    private readonly ILogger<TelegramBackgroundService> _logger;
    private readonly TelegramBotClient _botClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly CancellationTokenSource _cts = new();
    private static ReceiverOptions ReceiverOptions = new()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };

    public TelegramBackgroundService(IOptions<TelegramClientOptions> options,
        ILogger<TelegramBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;


        if (string.IsNullOrWhiteSpace(options.Value.AccessToken))
        {
            throw new InvalidConfigurationException("Telegram access token is not defined");
        }

        _botClient = new TelegramBotClient(options.Value.AccessToken);
    }

    public async Task SendMessageAsync(Recepient recepient, string messageText, TelegramSession session, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
                chatId: session.ChatId,
                text: messageText,
                cancellationToken: cancellationToken);
    }

    public async Task SendMessageAsync(Recepient recepient, string messageText, TelegramSession session, 
        Stream attachmentStream, CancellationToken cancellationToken)
    {
        await _botClient.SendDocumentAsync(
                chatId: session.ChatId,
                document: InputFile.FromStream(attachmentStream, "report.pdf"),
                caption: "Report",
                cancellationToken: cancellationToken);
    }

    public Task StartListenAsync()
    {
        _botClient.StartReceiving(
            updateHandler: this,
            receiverOptions: ReceiverOptions,
            cancellationToken: _cts.Token
        );

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { })
            return;

        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var telegramSessionsRepository = scope.ServiceProvider.GetRequiredService<ITelegramSessionsRepository>();
                long chatId = message.Chat.Id;
                var hasSession = await HasSessionWithChatIdAsync(chatId, telegramSessionsRepository);

                if (!hasSession)
                {
                    string userName = message.Chat.Username ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        _logger.LogWarning("User name {Id} is empty", message.Chat.Id);
                        return;
                    }

                    var recepientSource = scope.ServiceProvider.GetService<IRecepientsSource>();

                    Recepient recepient = await recepientSource.FindAsync(ContactType.Telegram, userName);

                    if (recepient == null)
                    {
                        _logger.LogInformation("User is not found {UserName}", userName);
                        return;
                    }

                    telegramSessionsRepository.Create(new TelegramSession()
                    {
                        ChatId = chatId,
                        RecepientId = recepient.RecepientId,
                        Id = Guid.NewGuid(),
                    });

                    _logger.LogInformation("Session was created for {ChatId}", chatId);

                    await telegramSessionsRepository.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartListenAsync();
    }

    public async Task<bool> HasSessionWithChatIdAsync(long chatId, ITelegramSessionsRepository telegramSessionsRepository)
    {
        return await telegramSessionsRepository.FindSessionByChatIdAsync(chatId) != null;
    }

    public override void Dispose()
    {
        _cts.Dispose();
        base.Dispose();
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
