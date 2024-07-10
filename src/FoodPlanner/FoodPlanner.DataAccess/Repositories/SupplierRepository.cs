using FoodPlanner.DataAccess.Entities;
using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Utils;

namespace FoodPlanner.DataAccess.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private static readonly string ProductActualPriceApiUrl = "api/Prices/GetLastByProduct/";

    private readonly HttpClient _httpClient;

    public SupplierRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FoodSupplierApi");
    }

    public async Task<PriceEntity> GetActualProductPriceAsync(Guid productId)
    {
        var price = new PriceEntity();

        var priceJson = await _httpClient.GetStringAsync(ProductActualPriceApiUrl + $"?Id={productId}");
        var priceDeserialized = JsonPriceConverter.Convert(priceJson);

        if (priceDeserialized != null)
            price = priceDeserialized;        

        return price;
    }
}