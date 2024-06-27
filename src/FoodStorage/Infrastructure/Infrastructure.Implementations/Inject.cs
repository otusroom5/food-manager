using FoodStorage.Application.Repositories;
using FoodStorage.Application.Repositories.MessageBroker;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.Implementations.MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStorage.Infrastructure.Implementations;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(o => o.UseNpgsql(configuration["DbConnection"]));

        services.AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IProductItemRepository, ProductItemRepository>()
                .AddScoped<IProductHistoryRepository, ProductHistoryRepository>()
                .AddScoped<IRecipeRepository, RecipeRepository>()
                .AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<IMessageProducer, MessageProducer>();

        return services;
    }
}
