using FoodUserNotifier.BusinessLogic.Abstractions;
using FoodUserNotifier.BusinessLogic.Interfaces;
using System.Text.Json;

namespace FoodUserNotifier.BusinessLogic.Services;

public class JsonMessageConverter : IMessageConverter
{
    public DataMessage Convert(string message)
    {
        return JsonSerializer.Deserialize<DataMessage>(message);
    }
}
