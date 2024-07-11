using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Sources;
using FoodUserNotifier.Infrastructure.Sources.Interfaces;
using FoodUserNotifier.Infrastructure.Telegram.Services.Implementations;
using FoodUserNotifier.Infrastructure.Telegram.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace FoodUserNotifier.Infrastructure.Sender.Telegram;

public sealed class TelegramMessageSender : IMessageSender
{
    private readonly ITelegramBackgroundService _telegramService;
    private readonly ITelegramSessionsRepository _telegramSessionsRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public TelegramMessageSender(ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider,
        ITelegramSessionsRepository telegramSessionsRepository)
    {
        _serviceProvider = serviceProvider;
        _telegramService = _serviceProvider.GetServices<IHostedService>().OfType<TelegramBackgroundService>().Single();
        _telegramSessionsRepository = telegramSessionsRepository;
        _logger = loggerFactory.CreateLogger<TelegramMessageSender>();
    }

    public async Task SendAsync(Core.Entities.Message message, DeliveryReport report)
    { 
        var exceptions = new ConcurrentQueue<Exception>();

        IEnumerable<Recepient> telegramRecepients = message.Recepients.Where(f => f.ContactType == Core.Entities.Types.ContactType.Telegram);

        IEnumerable<Stream> reportsStream = await DownloadReportsAsync(message.AttachmentIds);

        Parallel.ForEach(telegramRecepients, async recepient =>
        {
            using var cts = new CancellationTokenSource();

            try
            {
                TelegramSession session = await _telegramSessionsRepository.GetSessionByRecepientIdAsync(recepient.RecepientId);

                if (session == null) 
                {

                    _logger.LogWarning("Session not found for recepientId {1}", recepient.RecepientId);
                    return;
                }

                await _telegramService.SendMessageAsync(recepient, message.MessageText, session, cts.Token);

                
                foreach (var reportStream in reportsStream)
                {
                    await _telegramService.SendMessageAsync(recepient, message.MessageText, session,
                        reportStream, cts.Token);
                }


                _logger.LogTrace("Message was send successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                exceptions.Enqueue(ex);
            }
        });

        
        if (!exceptions.IsEmpty)
        {
            throw new AggregateException($"Aggregate Exception (NotificationId: {report.NotificationId})", exceptions);
        }
    }

    public async Task<IEnumerable<Stream>> DownloadReportsAsync(Guid[] AttachmentIds)
    {
        var reportsStream = new ConcurrentBag<Stream>();
        var exceptions = new ConcurrentQueue<Exception>();

        await Parallel.ForEachAsync(AttachmentIds, async (attacmnetId, token) =>
        {
            try
            {
                var reportSource = _serviceProvider.GetRequiredService<IReportsSource>();
                Stream stream = await reportSource.GetReportAsync(attacmnetId);
                reportsStream.Add(stream);

                _logger.LogTrace("Message was send successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                exceptions.Enqueue(ex);
            }
        });

        if (!exceptions.IsEmpty)
        {
            throw new AggregateException($"Aggregate Exception", exceptions);
        }

        return reportsStream.ToArray();
    }


}
