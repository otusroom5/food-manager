using FoodStorage.Application.Implementations.Common;
using FoodStorage.Application.Implementations.DomainEventHandlers;
using FoodStorage.Application.Implementations.Services;
using FoodStorage.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStorage.Application.Implementations;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>()
                .AddScoped<IProductItemService, ProductItemService>()
                .AddScoped<IRecipeService, RecipeService>()
                .AddScoped<IProductHistoryService, ProductHistoryService>()
                .AddScoped<IUnitService, UnitService>();

        services.AddDomainEventsHandling();
        services.AddBackgroundServices(configuration);

        return services;
    }

    /// <summary>
    /// Обработка доменных событий
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddDomainEventsHandling(this IServiceCollection services)
    {
        // Поиск типов, которые унаследованы от BaseDomainEventHandler<>.
        Type baseType = typeof(BaseDomainEventHandler<>);
        var anyDomainEvent = baseType.Assembly.ExportedTypes.FirstOrDefault(t => t.BaseType.Name == baseType.Name);

        // Если найден хоть какой-то существующий потомок от BaseDomainEventHandler<> - берём всю сборку этого типа
        return anyDomainEvent is null
            ? services
            : services.AddMediatR(c => c.RegisterServicesFromAssemblies(anyDomainEvent.Assembly));
    }

    /// <summary>
    /// Регистрация фоновых задач 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        CheckExpiredProductsConfiguration checkExpiredProductsConfiguration = new();

        configuration
            .GetSection(CheckExpiredProductsConfiguration.ReportExpireProductsConfig)
            .Bind(checkExpiredProductsConfiguration);

        return services.AddHostedService<CheckExpiredProductsBackgroundService>().AddSingleton(checkExpiredProductsConfiguration);
    }
}
