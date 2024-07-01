using FoodSupplier.BusinessLogic.Abstractions;
using Microsoft.Extensions.Logging;

namespace FoodSupplier.BusinessLogic.Services;

public class SupplierService : ISupplierService
{
    private readonly IPriceCollector _priceCollector;
    private readonly IPricesService _pricesService;
    private readonly IShopsService _shopsService;
    private readonly IFoodStorageGateway _storageGateway;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(IPriceCollector priceCollector,
        IPricesService pricesService,
        IShopsService shopsService,
        ILogger<SupplierService> logger,
        IFoodStorageGateway storageGateway)
    {
        _priceCollector = priceCollector;
        _pricesService = pricesService;
        _shopsService = shopsService;
        _logger = logger;
        _storageGateway = storageGateway;
    }

    public async Task ProduceAsync()
    {
        var shops = await _shopsService.GetAllAsync(true);
        var products = await _storageGateway.GetAllProductsAsync();

        foreach (var shop in shops)
        {
            foreach (var product in products)
            {
                await ProduceAsync(shop.Id, product.Id);
            }
        }
    }

    public async Task ProduceAsync(Guid shopId, Guid productId)
    {
        var priceEntry = _priceCollector.Collect(shopId, productId);
        var result = await _pricesService.CreateAsync(priceEntry);

        _logger.LogInformation("PriceEntry created: {PriceEntryId}", result);
    }
}