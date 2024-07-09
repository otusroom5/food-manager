using FoodPlanner.BusinessLogic.Models;
using System.Runtime.Serialization;
using System.Text.Json;


namespace FoodPlanner.BusinessLogic.Converters;

public static class JsonExpireProductConverter
{
    public static ExpireProduct Convert(string productsJson)
    {
        try
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ExpireProduct>(productsJson, options: jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
