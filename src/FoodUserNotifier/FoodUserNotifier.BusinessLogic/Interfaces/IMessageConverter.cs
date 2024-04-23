using FoodUserNotifier.BusinessLogic.Abstractions;

namespace FoodUserNotifier.BusinessLogic.Interfaces;

public interface IMessageConverter
{
    DataMessage Convert(string message);
}
