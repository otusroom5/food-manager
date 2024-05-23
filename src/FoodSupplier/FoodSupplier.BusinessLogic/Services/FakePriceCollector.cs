using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace FoodSupplier.BusinessLogic.Services;

public class FakePriceCollector : IPriceCollector
{
    private readonly Random _random = new();
    private readonly ILogger<PriceEntry> _logger;

    public FakePriceCollector(ILogger<PriceEntry> logger)
    {
        _logger = logger;
    }

    public PriceEntry Collect(Guid shopId, Guid productId)
    {
        var price = (decimal) (_random.NextDouble() * 100);

        var priceEntry = new PriceEntry(
            productId: productId,
            shopId: shopId,
            date: DateTimeOffset.UtcNow,
            price: price);

        _logger.LogDebug("Fake PriceEntry generated for ProductId: {ProductId}, price: {Price}", productId, price);

        return priceEntry;
    }
}