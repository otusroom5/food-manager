using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodUserNotifier.Infrastructure.Services.Implementations;

public class JsonNotificationConverter : INotificationConverter
{
    public Notification Convert(string message)
    {
        try
        { 
            var jsonOptions = new System.Text.Json.JsonSerializerOptions();
            jsonOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<Notification>(message, options: jsonOptions);
        } 
        catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
