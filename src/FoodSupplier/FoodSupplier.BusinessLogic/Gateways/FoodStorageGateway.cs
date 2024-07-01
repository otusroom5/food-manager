using System.Text.Json;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace FoodSupplier.BusinessLogic.Gateways;

public class FoodStorageGateway : IFoodStorageGateway
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FoodStorageGateway> _logger;

    public FoodStorageGateway(HttpClient httpClient,
        ILogger<FoodStorageGateway> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var productsJson = await _httpClient.GetStringAsync("api/Product/GetAll");
        _logger.LogDebug(productsJson);
        var products = JsonSerializer.Deserialize<List<Product>>(productsJson);

        return products;
    }
}