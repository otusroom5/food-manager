using FoodStorage.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStorage.Application.Implementations;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>()
                .AddScoped<IProductItemService, ProductItemService>()
                .AddScoped<IRecipeService, RecipeService>();

        return services;
    }
}
