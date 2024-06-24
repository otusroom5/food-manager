using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Interfaces;

namespace FoodUserNotifier.Application.WebAPI.Utils;

public class MessageSenderCollection : IMessageSenderCollection
{
    private readonly IServiceProvider _serviceProvider;
    public MessageSenderCollection(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<IMessageSender> GetMessageSenders()
    {
        return _serviceProvider.GetServices<IMessageSender>();
    }
}
