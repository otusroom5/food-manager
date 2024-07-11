using System.Runtime.Serialization;
using System.Text.Json;
using FoodPlanner.DataAccess.Entities;

namespace FoodPlanner.DataAccess.Utils;

public static class JsonShopConverter
{
    public static ShopEntity Convert(string shopJson)
    {
        try
        {        
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true             
            };  

            return JsonSerializer.Deserialize<ShopEntity>(shopJson, options: jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
