using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sources;
using FoodUserNotifier.Infrastructure.Sources.Interfaces;

namespace FoodUserNotifier.Application.WebAPI.Extensions;

public static class SeviceCollectionExtensions
{
    public static IServiceCollection AddRecepientsSource(this IServiceCollection serviceDescriptors, string serviceName)
    {
        return serviceDescriptors.AddTransient<IRecepientsSource>(provider =>
        {
            IHttpClientFactory httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            ILogger<RecepientsSource> logger = provider.GetRequiredService<ILogger<RecepientsSource>>();
            
            return new RecepientsSource(httpClientFactory, serviceName, logger);
        });
    }

    public static IServiceCollection AddReportsSource(this IServiceCollection serviceDescriptors, string serviceName)
    {
        return serviceDescriptors.AddTransient<IReportsSource>(provider =>
        {
            IHttpClientFactory httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            ILogger<ReportsSource> logger = provider.GetRequiredService<ILogger<ReportsSource>>();

            return new ReportsSource(httpClientFactory, serviceName, logger);
        });
    }


    
}
