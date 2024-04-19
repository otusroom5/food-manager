using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;

namespace FoodUserNotifier.WebApi.Implementations
{
    public class SmtpMessageSender : IMessageSender
    {
        private readonly IOptions<SmptClientOptions> _options;
        private readonly ILogger _logger;
        public SmtpMessageSender(IOptions<SmptClientOptions> options, ILogger logger) 
        {
            _options = options;
            _logger = logger;
        }

        public Task SendAsync(IEnumerable<Recepient> recepients, string message)
        {
            throw new NotImplementedException();
        }
    }
}
