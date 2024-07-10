using System.Runtime.Serialization;
using System.Text.Json;
using FoodPlanner.DataAccess.Entities;

namespace FoodPlanner.DataAccess.Utils;

public static class JsonPriceConverter
{
    public static PriceEntity Convert(string productsJson)
    {
        try
        {        
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true             
            };  

            return JsonSerializer.Deserialize<PriceEntity>(productsJson, options: jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
