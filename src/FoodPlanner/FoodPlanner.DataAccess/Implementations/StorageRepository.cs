using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Models;
using System.Text.Json;

namespace FoodPlanner.DataAccess.Implementations;

public class StorageRepository: IStorageRepository
{
    private readonly HttpClient _httpClient;

    public StorageRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetExpiredProductsAsync()
    {
        var products = new List<ProductDto>();

        var productsJson = await _httpClient.GetStringAsync("api/ProductItem/GetExpiredProductItems");
        var productsDeserialized = JsonSerializer.Deserialize<List<ProductDto>>(productsJson);

        if (productsDeserialized != null)
        {
            products.AddRange(productsDeserialized);
        }

        return products;
    }   
}
