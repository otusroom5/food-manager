using FoodManager.Shared.Utils;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FoodManager.WebUI.Extensions;

public static class HttpClientWebApplicationExtensions
{
    public readonly static string DefaultServiceSchema = "http";

    public static IHttpClientBuilder AddHttpClient(this IServiceCollection collection, string serviceName, string connectionString)
    {
        collection.AddTransient<AutorizationTokenHttpMessageHandler>();

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
            }).AddHttpMessageHandler<AutorizationTokenHttpMessageHandler>();
    }
}

public class AutorizationTokenHttpMessageHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AutorizationTokenHttpMessageHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string token = GetToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
       
        return base.SendAsync(request, cancellationToken);
    }

    private string GetToken()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.UserData))?.Value ?? string.Empty;
    }

}
