using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Services;

namespace FoodSupplier.WebAPI.Modules;

public static class Services
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPriceCollector, FakePriceCollector>();
        serviceCollection.AddScoped<IPricesService, PricesService>();
        serviceCollection.AddScoped<IShopsService, ShopsService>();
        serviceCollection.AddScoped<ISupplierService, SupplierService>();
    }
}