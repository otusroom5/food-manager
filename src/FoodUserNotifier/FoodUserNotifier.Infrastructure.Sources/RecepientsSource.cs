using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Core.Interfaces.Sources;
using FoodUserNotifier.Infrastructure.Sources.Contracts;
using FoodUserNotifier.Infrastructure.Sources.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace FoodUserNotifier.Infrastructure.Sources;

public partial class RecepientsSource : IRecepientsSource
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _serviceName;

    public RecepientsSource(IHttpClientFactory httpClientFactory, string serviceName)
    {
        _serviceName = string.IsNullOrWhiteSpace(serviceName) ? throw new ArgumentException(nameof(serviceName)) : serviceName;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Recepient>> GetAllByRecepientGroup(RecepientGroupType recepientGroupType)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient(_serviceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = "api/v1/UserContacts/GetAllForRole",
            Query = QueryString.Create("role", recepientGroupType.ToString()).ToString()
        }.Uri;

        HttpResponseMessage response = await httpClient.GetAsync(requestUri);

        RecepientResponse recepientResponse = await response.Content.ReadFromJsonAsync<RecepientResponse>();

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
            throw new HttpRequestException(recepientResponse.Message, ex);
        }

        return recepientResponse.Data.Select(item => new Recepient()
        {
            Id = item.Id,
            ContactType = (ContactType) item.ContactType,
            Address = item.Contact
        }).ToArray();
    }
}