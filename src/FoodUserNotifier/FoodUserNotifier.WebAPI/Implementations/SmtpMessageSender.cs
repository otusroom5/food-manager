using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace FoodUserNotifier.WebApi.Implementations;

public class SmtpMessageSender : IMessageSender
{
    private readonly IOptions<SmptClientOptions> _options;
    private readonly ILogger _logger;
    public SmtpMessageSender(IOptions<SmptClientOptions> options, ILogger<SmtpMessageSender> logger) 
    {
        _options = options;
        _logger = logger;
    }

    public Task SendAsync(IEnumerable<Recepient> recepients, string message)
    {
        Parallel.ForEach(recepients, async recepient =>
        {
            await SendAsync(recepient, message);
        });

        return Task.CompletedTask;
    }

    private async Task SendAsync(Recepient recepient, string textMessage)
    {
        using SmtpClient client = new SmtpClient(_options.Value.HostName);
        MailAddress from = new MailAddress(_options.Value.SenderAddress);
        MailAddress to = new MailAddress(recepient.EmailAddress);
        
        using MailMessage message = new MailMessage(from, to)
        {
            Body = textMessage,
            BodyEncoding = System.Text.Encoding.UTF8,
            Subject = _options.Value.MailSubject,
            SubjectEncoding = System.Text.Encoding.UTF8
        };

        await client.SendMailAsync(message);

        _logger.LogTrace($"Mail { recepient.EmailAddress } was send");
    }
}
