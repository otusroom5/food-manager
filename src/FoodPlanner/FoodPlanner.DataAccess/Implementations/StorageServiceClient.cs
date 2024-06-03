using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Models;
using System.Text.Json;

namespace FoodPlanner.DataAccess.Implementations;

public class StorageServiceClient: IStorageServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _url;
    public StorageServiceClient(HttpClient httpClient, string url)
    {
        _httpClient = httpClient;
        _url = url;
    }

    public async Task<List<ProductDto>> GetExpiredProductsAsync()
    {
        var result = new List<ProductDto>();

        var client = await _httpClient.GetAsync(_url);
        client.EnsureSuccessStatusCode();
        if (client.IsSuccessStatusCode)
        {
            var jsonString = await client.Content.ReadAsStringAsync();            
            var response = JsonSerializer.Deserialize<List<ProductDto>>(jsonString);
            if (response != null)
            {
                result.AddRange(response);
            }
        }

        return result;
    }
}
