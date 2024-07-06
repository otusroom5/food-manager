using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Models;
using System.Text.Json;

namespace FoodPlanner.DataAccess.Implementations;

public class StorageRepository : IStorageRepository
{
    private static readonly string ExpiredProductsApiUrl = "api/ProductItem/GetExpiredProductItems";

    private readonly HttpClient _httpClient;

    public StorageRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FoodStorageApi");
    }

    public async Task<List<ProductEntity>> GetExpiredProductsAsync()
    {
        var products = new List<ProductEntity>();

        var productsJson = await _httpClient.GetStringAsync(ExpiredProductsApiUrl);
        var productsDeserialized = JsonSerializer.Deserialize<List<ProductEntity>>(productsJson);

        if (productsDeserialized != null)
        {
            products.AddRange(productsDeserialized);
        }

        return products;
    }
}