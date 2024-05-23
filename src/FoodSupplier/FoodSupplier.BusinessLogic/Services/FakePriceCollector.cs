using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Services;

public class FakePriceCollector : IPriceCollector
{
    private readonly Random _random = new();

    public PriceEntry Collect(Guid shopId, Guid productId)
    {
        var price = (decimal) (_random.NextDouble() * 100);

        var priceEntry = new PriceEntry(
            productId: productId,
            shopId: shopId,
            date: DateTimeOffset.UtcNow,
            price: price);

        return priceEntry;
    }
}