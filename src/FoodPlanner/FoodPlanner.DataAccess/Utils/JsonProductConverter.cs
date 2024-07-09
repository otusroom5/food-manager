using System.Runtime.Serialization;
using System.Text.Json;
using FoodPlanner.DataAccess.Models;

namespace FoodPlanner.DataAccess.Utils;

public static class JsonProductConverter
{
    public static List<ProductEntity> Convert(string productsJson)
    {
        try
        {        
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true             
            };  

            return JsonSerializer.Deserialize<List<ProductEntity>>(productsJson, options: jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new SerializationException(ex.Message, ex);
        }
    }
}
