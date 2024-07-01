using System.Text.Json.Serialization;

namespace FoodSupplier.BusinessLogic.Models;

public class Product
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}