using FoodPlanner.DataAccess.Entities;
using FoodPlanner.DataAccess.Interfaces;
using FoodPlanner.DataAccess.Utils;

namespace FoodPlanner.DataAccess.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private static readonly string ProductActualPriceApiUrl = "api/Prices/GetLastByProduct/";
    private static readonly string ShopsApiUrl = "api/Shops/";

    private readonly HttpClient _httpClient;

    public SupplierRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FoodSupplierApi");
    }

    public async Task<PriceEntity> GetActualProductPriceAsync(Guid productId)
    {
        var priceEntity = new PriceEntity();

        var priceJson = await _httpClient.GetStringAsync(ProductActualPriceApiUrl + $"?Id={productId}");
        if (!string.IsNullOrEmpty(priceJson))
        {
            var priceDeserialized = JsonPriceConverter.Convert(priceJson);

            if (priceDeserialized != null)
                priceEntity = priceDeserialized;
        }

        return priceEntity;
    }

    public async Task<ShopEntity> GetShopDetailsAsync(Guid shopId)
    {
        var shopEntity = new ShopEntity();

        var shopJson = await _httpClient.GetStringAsync(ShopsApiUrl + $"?Id={shopId}");
        if (!string.IsNullOrEmpty(shopJson))
        {
            var shopDeserialized = JsonShopConverter.Convert(shopJson);

            if (shopDeserialized != null)
                shopEntity = shopDeserialized;
        }

        return shopEntity;
    }
}