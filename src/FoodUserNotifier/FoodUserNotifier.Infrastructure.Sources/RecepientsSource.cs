using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sources.Contracts;
using FoodUserNotifier.Infrastructure.Sources.Contracts.Requests;
using FoodUserNotifier.Infrastructure.Sources.Exceptions;
using FoodUserNotifier.Infrastructure.Sources.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodUserNotifier.Infrastructure.Sources;

public partial class RecepientsSource : IRecepientsSource
{
    private const string UserAuthGetAllForRoleUrl = "/api/v1/UserContacts/GetAllForRole";
    private const string UserAuthGetContact = "/api/v1/UserContacts/GetContact";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _serviceName;
    private static JsonSerializerOptions _serializerOptions;

    public RecepientsSource(IHttpClientFactory httpClientFactory, string serviceName)
    {
        _serviceName = string.IsNullOrWhiteSpace(serviceName) ? throw new ArgumentException(nameof(serviceName)) : serviceName;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Recepient> FindAsync(ContactType contactType, string contact)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient(_serviceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UserAuthGetContact,
            Query = new FindRecepientRequest()
            {
                ContactType = contactType,
                Contact = contact   
            }.ToQueryString().ToString()
        }.Uri;

        HttpResponseMessage response = await httpClient.GetAsync(requestUri);
        var recepientResponse = await response.Content.ReadFromJsonAsync<GenericResponse<RecepientModel>>(GetJsonSerializerOptions());

        if (recepientResponse is null)
        {
            throw new InvalidServiceResponseException();
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException(recepientResponse.Message ?? ex.Message, ex);
        }

        if (recepientResponse.Data == null)
        {
            return null;
        }

        return new Recepient()
        {
            Id = recepientResponse.Data.Id,
            RecepientId = recepientResponse.Data.UserId,
            ContactType = recepientResponse.Data.ContactType,
            Address = recepientResponse.Data.Contact
        };
    }

    public async Task<IEnumerable<Recepient>> GetAllByRecepientGroupAsync(RecepientGroupType recepientGroupType)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient(_serviceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UserAuthGetAllForRoleUrl,
            Query = QueryString.Create("role", recepientGroupType.ToString()).ToString()
        }.Uri;

        HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
        var response = await httpResponse.Content.ReadFromJsonAsync<GenericResponse<IEnumerable<RecepientModel>>>(GetJsonSerializerOptions());

        if (response is null)
        {
            throw new InvalidServiceResponseException();
        }

        try
        {
            httpResponse.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException(response.Message ?? ex.Message, ex);
        }

        return response.Data.Select(item => new Recepient()
        {
            Id = item.Id,
            RecepientId = item.UserId,
            ContactType = item.ContactType,
            Address = item.Contact
        }).ToArray();
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        if (_serializerOptions != null)
        {
            return _serializerOptions;
        }

        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        
        return _serializerOptions;
    }
}