using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Interfaces;

namespace FoodUserNotifier.Application.WebAPI.Utils
{
    public class MessageSenderCollection : IMessageSenderCollection
    {
        public IEnumerable<IMessageSender> GetMessageSenders()
        {
            throw new NotImplementedException();
        }
    }
}
