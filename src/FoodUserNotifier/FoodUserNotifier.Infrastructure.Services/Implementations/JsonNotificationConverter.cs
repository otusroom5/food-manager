using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json;

namespace FoodUserNotifier.Infrastructure.Services.Implementations;

public class JsonNotificationConverter : INotificationConverter
{
    public Notification Convert(string message)
    {
        try
        {
            return JsonSerializer.Deserialize<Notification>(message);
        
        } catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
