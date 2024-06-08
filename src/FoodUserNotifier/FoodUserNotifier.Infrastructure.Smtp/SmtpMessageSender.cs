
using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Sender.Smtp.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FoodUserNotifier.Infrastructure.Sender.Smtp;

public sealed class SmtpMessageSender : IMessageSender
{
    private readonly IOptions<SmptClientOptions> _options;
    private readonly ILogger _logger;

    public SmtpMessageSender(IOptions<SmptClientOptions> options, ILoggerFactory loggerFactory) 
    {
        _options = options;
        _logger = loggerFactory.CreateLogger<SmtpMessageSender>();
    }

    public Task SendAsync(Message message, Report report)
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }
}
