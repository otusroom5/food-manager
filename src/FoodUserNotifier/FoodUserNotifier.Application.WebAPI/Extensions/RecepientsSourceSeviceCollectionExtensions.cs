using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sources;

namespace FoodUserNotifier.Application.WebAPI.Extensions;

public static class RecepientsSourceSeviceCollectionExtensions
{
    public static IServiceCollection AddRecepientsSource(this IServiceCollection serviceDescriptors, string serviceName)
    {
        return serviceDescriptors.AddTransient<IRecepientsSource>(provider =>
        {
            IHttpClientFactory httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            return new RecepientsSource(httpClientFactory, serviceName);
        });
    }
}
