using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;

namespace FoodUserNotifier.WebApi.Implementations
{
    public class TelegramMessageSender : IMessageSender
    {
        private readonly IOptions<TelegramClientOptions> _options;
        private readonly ILogger _logger;
        public TelegramMessageSender(IOptions<TelegramClientOptions> options, ILogger logger)
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
