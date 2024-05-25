using FoodManager.Shared.Utils;

namespace FoodManager.WebUI.Extensions;

public static class HttpClientWebApplicationExtensions
{
    public readonly static string DefaultServiceSchema = "http";

    public static IHttpClientBuilder AddHttpClient(this IServiceCollection collection, string serviceName, string connectionString)
    {
        return collection.AddHttpClient(name: serviceName,
            configureClient: options =>
            {
                var serviceConnection = ServiceConnectionBuilder.Parce(connectionString);

                options.BaseAddress = new UriBuilder()
                {
                    Host = serviceConnection.GetHost(),
                    Port = serviceConnection.GetPort(),
                    Scheme = serviceConnection.GetSchema(DefaultServiceSchema),
                }.Uri;
            });
    }

}
