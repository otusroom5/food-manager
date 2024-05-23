using System.Security.AccessControl;
using FoodSupplier.BusinessLogic.Abstractions;
using Microsoft.Extensions.Logging;

namespace FoodSupplier.BusinessLogic.Services;

public class SupplierService : ISupplierService
{
    private readonly IPriceCollector _priceCollector;
    private readonly IPricesService _pricesService;
    private readonly IShopsService _shopsService;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(IPriceCollector priceCollector,
        IPricesService pricesService,
        IShopsService shopsService,
        ILogger<SupplierService> logger)
    {
        _priceCollector = priceCollector;
        _pricesService = pricesService;
        _shopsService = shopsService;
        _logger = logger;
    }

    public void Produce()
    {
        var shops = _shopsService.GetAll(true);
        var productsIds = new List<Guid>(); //todo implement FoodStorageClient

        foreach (var shop in shops)
        {
            foreach (var productId in productsIds)
            {
                Produce(shop.Id, productId);
            }
        }
    }

    public void Produce(Guid shopId, Guid productId)
    {
        var priceEntry = _priceCollector.Collect(shopId, productId);
        var result = _pricesService.Create(priceEntry);

        _logger.LogInformation("PriceEntry created: {PriceEntryId}", result);
    }
}