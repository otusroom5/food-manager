using Microsoft.IdentityModel.Protocols.Configuration;
using System.Net;
using FoodManager.Shared.Exceptions;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using FoodManager.Shared.Utils.Interfaces;
using FoodManager.Shared.Utils.Implementations;

namespace FoodManager.Shared.Extensions;

public static class HttpClientServiceCollectionExtensions
{
    public const string DefaultServiceSchema = "http";
    public static string AuthServiceNameHeader = "AuthServiceName";

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

                if (!string.IsNullOrWhiteSpace(httpClientOptions.AuthServiceName))
                {
                    configure.DefaultRequestHeaders.Add(AuthServiceNameHeader, httpClientOptions.AuthServiceName);

                    if (!string.IsNullOrWhiteSpace(httpClientOptions.ApiKey))
                    {
                        configure.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiAuthenticationAuthenticationBuilderExtensions.ApiKeyAuthentication,
                            httpClientOptions.ApiKey);
                    }
                }
            });

        switch (httpClientOptions.AuthenticationType)
        {
            case AuthenticationType.ApiKey:
                clientBuilder.AddHttpMessageHandler<ApiKeyAuthenticationHttpMessageHandler>();
                break;
            case AuthenticationType.Bearer:
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

    
}

internal class BearerAuthenticationHttpMessageHandler : DelegatingHandler
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

internal class ApiKeyAuthenticationHttpMessageHandler : DelegatingHandler
{
    private const string RenewTokenUrl = "/api/v1/ApiKeys/RenewToken";
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiKeyAuthenticationHttpMessageHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!request.Headers.TryGetValues(HttpClientServiceCollectionExtensions.AuthServiceNameHeader, out var values))
            {
                throw new InvalidConfigurationException($"{HttpClientServiceCollectionExtensions.AuthServiceNameHeader} header is not defiend");
            }

            if (!values.Any())
            {
                throw new InvalidConfigurationException($"{HttpClientServiceCollectionExtensions.AuthServiceNameHeader} header value is not defiend");
            }

            string authServiceName = values.First();

            var apiKey = await GetRenewTokenAsync(authServiceName, request.Headers.Authorization.Parameter);
            request.Headers.Authorization = new AuthenticationHeaderValue(ApiAuthenticationAuthenticationBuilderExtensions.ApiKeyAuthentication, apiKey);

            return await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    private async Task<string> GetRenewTokenAsync(string authServiceName, string oldToken)
    {
        HttpClient client = _httpClientFactory.CreateClient(authServiceName);

        try
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(RenewTokenUrl, new
            {
                OldToken = oldToken
            });

            ApiKeyRenewResponse response = await responseMessage.Content.ReadFromJsonAsync<ApiKeyRenewResponse>();

            try
            {
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"{response.Message} ({ex.Message})");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response.Data;
        }
        catch (HttpRequestException ex)
        {
            throw new ApiTokenUpdateException(ex.Message, ex);
        }
    }

    public sealed class ApiKeyRenewResponse
    {
        public string Data { get; set; }
        public string Message { get; set; }
    }
}

public sealed class HttpClientOptions
{
    public string ServiceName { get; set; }
    public string ConnectionString { get; set; }
    public string AuthServiceName { get; set; }
    public string ApiKey { get; set; }
    public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Bearer;

}

public enum AuthenticationType { None, Bearer, ApiKey }