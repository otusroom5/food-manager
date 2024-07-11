using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Models;
using FoodPlanner.DataAccess.Utils;

namespace FoodPlanner.DataAccess.Repositories;

public class StorageRepository : IStorageRepository
{
    private static readonly string ExpiredProductsApiUrl = "api/ProductItem/GetExpiredProductItems/";

    private readonly HttpClient _httpClient;

    public StorageRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FoodStorageApi");
    }

    public async Task<List<ProductEntity>> GetExpiredProductsAsync(int daysBeforeExpired)
    {
        var products = new List<ProductEntity>();

        var productsJson = await _httpClient.GetStringAsync(ExpiredProductsApiUrl+ $"?daysBeforeExpired={daysBeforeExpired}");
        var productsDeserialized = JsonProductConverter.Convert(productsJson);

        if (productsDeserialized != null)        
            products.AddRange(productsDeserialized);        

        return products;
    }
}