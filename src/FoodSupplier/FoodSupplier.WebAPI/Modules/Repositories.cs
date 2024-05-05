using FoodSupplier.DataAccess.Abstractions;
using FoodSupplier.DataAccess.Repositories;

namespace FoodSupplier.WebAPI.Modules;

public static class Repositories
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IShopsRepository, ShopsRepository>();
        serviceCollection.AddScoped<IPricesRepository, PricesRepository>();
    }
}