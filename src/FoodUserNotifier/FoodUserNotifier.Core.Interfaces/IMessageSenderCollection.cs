using FoodUserNotifier.Core.Domain.Interfaces;

namespace FoodUserNotifier.Core.Interfaces;

public interface IMessageSenderCollection
{
    public IEnumerable<IMessageSender> GetMessageSenders();
}
