using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;
using FoodUserNotifier.BusinessLogic.Contracts;

namespace FoodUserNotifier.WebApi.Implementations;

public class SmtpMessageSender : IMessageSender
{
    private readonly IOptions<SmptClientOptions> _options;
    private readonly ILogger _logger;
    public SmtpMessageSender(IOptions<SmptClientOptions> options, ILogger logger) 
    {
        _options = options;
        _logger = logger;
    }

    public Task SendAsync(IEnumerable<RecepientDTO> recepients, string message)
    {
        throw new NotImplementedException();
    }
}
