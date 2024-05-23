using FoodManager.Shared.Utils;

namespace FoodManager.WebUI.Extensions
{
    public static class HttpClientWebApplicationExtensions
    {
        public readonly static string AuthServiceName = "FoodAuthService";
        public readonly static int DefaultServicePort = 8081;
        public readonly static string DefaultServiceSchema = "http";

        public static IHttpClientBuilder AddAuthenticationHttpClient(this IServiceCollection collection, string connectionString)
        {
            return collection.AddHttpClient(name: AuthServiceName,
                configureClient: options =>
                {
                    var serviceConnection = ServiceConnectionBuilder.Parce(connectionString);

                    options.BaseAddress = new UriBuilder()
                    {
                        Host = serviceConnection.GetHost(),
                        Port = serviceConnection.GetPort(DefaultServicePort),
                        Scheme = serviceConnection.GetSchema(DefaultServiceSchema),
                    }.Uri;
                });
        }
    }
}
