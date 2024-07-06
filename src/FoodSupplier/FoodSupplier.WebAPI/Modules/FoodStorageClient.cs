using FoodManager.Shared.Extensions;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Gateways;

namespace FoodSupplier.WebAPI.Modules;

public static class FoodStorageClient
{
    public static void AddFoodStorageClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddTransient<IFoodStorageGateway, FoodStorageGateway>();

        serviceCollection.AddSwaggerGenWithBarerAuth();

        serviceCollection.AddHttpMessageHandlers();
        serviceCollection.AddHttpServiceClient("UserAuthApi", configuration.GetConnectionString("UserAuthApi"));
        serviceCollection.AddHttpServiceClient(options =>
        {
            options.ServiceName = "FoodStorageApi";
            options.ConnectionString = configuration.GetConnectionString("FoodStorageApi");
            options.AuthenticationType = AuthenticationType.ApiKey;
            options.AuthServiceName = "UserAuthApi";
            options.ApiKey = configuration.GetValue<string>("ApiKey");
        });

        serviceCollection.AddHttpClient<IFoodStorageGateway, FoodStorageGateway>("FoodStorageApi");
    }
}