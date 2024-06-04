using FoodManager.Shared.Utils;
using Microsoft.IdentityModel.Protocols.Configuration;
using System.Net;
using FoodManager.Shared.Exceptions;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace FoodManager.Shared.Extensions;

public static class HttpClientServiceCollectionExtensions
{
    public readonly static string DefaultServiceSchema = "http";

    public static IServiceCollection AddHttpMessageHandlers(this IServiceCollection collection)
    {
        collection.AddTransient<BearerAuthenticationHttpMessageHandler>();
        collection.AddTransient<ApiKeyAuthenticationHttpMessageHandler>();

        return collection;
    }

    public static IHttpClientBuilder AddHttpServiceClient(this IServiceCollection collection, Action<HttpClientOptions> options)
    {
        var httpClientOptions = new HttpClientOptions();
        options.Invoke(httpClientOptions);

        IHttpClientBuilder clientBuilder = collection.AddHttpClient(name: httpClientOptions.ServiceName,
            configureClient: configure =>
            {
                IServiceConnection serviceConnection = ServiceConnectionBuilder.Parce(httpClientOptions.ConnectionString);

                configure.BaseAddress = new UriBuilder()
                {
                    Host = serviceConnection.GetHost(),
                    Port = serviceConnection.GetPort(),
                    Scheme = serviceConnection.GetSchema(DefaultServiceSchema),
                }.Uri;

                if (httpClientOptions.AuthorizationHeader != null)
                {
                    configure.DefaultRequestHeaders.Authorization = httpClientOptions.AuthorizationHeader;
                }
            });

        switch (httpClientOptions.AuthenticationType)
        {
            case HttpClientOptions.HttpClientAuthentication.ApiKey:
                clientBuilder.AddHttpMessageHandler<ApiKeyAuthenticationHttpMessageHandler>();
                break;
            case HttpClientOptions.HttpClientAuthentication.Bearer:
                clientBuilder.AddHttpMessageHandler<BearerAuthenticationHttpMessageHandler>();
                break;
        }

        return clientBuilder;
    }

    public static IHttpClientBuilder AddHttpServiceClient(this IServiceCollection collection, string serviceName, string connectionString)
    {

        return collection.AddHttpServiceClient(options =>
        {
            options.ServiceName = serviceName;
            options.ConnectionString = connectionString;
        });        
    }

    public sealed class HttpClientOptions
    {
        public string ServiceName { get; set; }
        public string ConnectionString { get; set; }
        public AuthenticationHeaderValue AuthorizationHeader { get; set; }
        public HttpClientAuthentication AuthenticationType { get; set; } = HttpClientAuthentication.Bearer;
        public enum HttpClientAuthentication { None, Bearer, ApiKey }
    }
}

public class BearerAuthenticationHttpMessageHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public BearerAuthenticationHttpMessageHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string token = GetToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private string GetToken()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.UserData))?.Value ?? string.Empty;
    }
}

public class ApiKeyAuthenticationHttpMessageHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ApiKeyAuthenticationHttpMessageHandler(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var apiKey = await GetRenewTokenAsync(request.Headers.Authorization.Parameter);
            request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", apiKey);

            return await SendAsync(request, cancellationToken);
        }

        return response;
    }

    private async Task<string> GetRenewTokenAsync(string oldToken)
    {
        string serviceName = _configuration.GetValue<string>("ApiKeysApiServiceName");

        if (string.IsNullOrEmpty(serviceName))
        {
            throw new InvalidConfigurationException($"ApiKeysApiServiceName key is not defined");
        }

        HttpClient client = _httpClientFactory.CreateClient(serviceName);

        try
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/api/v1/ApiKeys/RenewToken", new
            {
                OldToken = oldToken
            });

            responseMessage.EnsureSuccessStatusCode();

            ApiKeyRenewResponse response = await responseMessage.Content.ReadFromJsonAsync<ApiKeyRenewResponse>();

            return response.Data;
        }
        catch (HttpRequestException ex)
        {
            throw new CommunicationException();
        }
    }

    public class ApiKeyRenewResponse
    {
        public string Data { get; set; }
        public string Message { get; set; }
    }
}