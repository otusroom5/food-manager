using FoodManager.Shared.Utils;
using FoodManager.WebUI.Contracts;
using FoodManager.WebUI.Exceptions;
using FoodManager.WebUI.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace FoodManager.WebUI.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public AccountService(IConfiguration configuration, ILogger<AccountService> logger)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<(string Token, string Role)> LogInAsync(string login, string password)
    {
        using HttpClient client = new HttpClient();

        var serviceConnection = ServiceConnectionBuilder.Parce(_configuration.GetConnectionString("UserAuthApi"));

        Uri requestUri = new UriBuilder()
        {
            Host = serviceConnection.GetHost(),
            Port = serviceConnection.GetPort(8081),
            Scheme = serviceConnection.GetSchema("http"),
            Path = $"/api/v{serviceConnection.GetVersionProtocol()}/Accounts/Login"
        }.Uri;

        var responseMessage = await client.PostAsync(requestUri, JsonContent.Create(new { LoginName = login, Password = password }));

        if ((responseMessage.StatusCode != HttpStatusCode.BadRequest) &&
            !responseMessage.IsSuccessStatusCode)
        {
            responseMessage.EnsureSuccessStatusCode();
        }

        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
        LoginResponse response = JsonSerializer.Deserialize<LoginResponse>(jsonResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new InvalidAccountException(response.Message);
        }
        
        return (response.Token, response.Role);
    } 
}
