using FoodSupplier.BusinessLogic.Abstractions;

namespace FoodSupplier.BusinessLogic.Services;

public class SupplierService : ISupplierService
{
    private readonly IPriceCollector _priceCollector;
    private readonly IPricesService _pricesService;
    private readonly IShopsService _shopsService;

    public SupplierService(IPriceCollector priceCollector, IPricesService pricesService, IShopsService shopsService)
    {
        _priceCollector = priceCollector;
        _pricesService = pricesService;
        _shopsService = shopsService;
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
        _pricesService.Create(priceEntry);
    }
}