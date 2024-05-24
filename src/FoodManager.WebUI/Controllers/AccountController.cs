using FoodManager.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using FoodManager.WebUI.Exceptions;
using FoodManager.WebUI.Contracts;
using FoodManager.WebUI.Extensions;
using System.Net;
using Serilog;
using System.ComponentModel.DataAnnotations;
using FoodManager.WebUI.Areas.Administrator.Contracts.Responses;
using System.Net.Http;

namespace FoodManager.WebUI.Controllers;

public class AccountController : Abstractions.ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAuthenticationService _authService;

    public AccountController(IHttpClientFactory httpClientFactory,
        IAuthenticationService authService,
        ILogger<AccountController> logger) : base(httpClientFactory)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(LoginModel model)
    {
        try
        {
            var acceptedData = await GetAuthTokenAsync(model.Login, model.Password);

            _logger.LogInformation("Account ({Login}) is accepted. Assigned following role: {Role}", model.Login, acceptedData.Data.Role);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Login),
                new Claim(ClaimTypes.NameIdentifier, acceptedData.Data.UserId),
                new Claim(ClaimTypes.Role, acceptedData.Data.Role),
                new Claim(ClaimTypes.UserData, acceptedData.Data.Token)
            };

            var claimsIdentity = new ClaimsIdentity(
              claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "Account/SignIn"
            };


            await _authService.SignInAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              authProperties);

            return RedirectToAction("", acceptedData.Data.Role);

        }
        catch (InvalidAccountException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async Task<AuthenticationResponse> GetAuthTokenAsync(string login, string password)
    {
        HttpClient client = CreateServiceHttpClient(HttpClientWebApplicationExtensions.AuthServiceName, false);

        Uri requestUri = new UriBuilder(client.BaseAddress)
        {
            Path = $"/api/v1/Accounts/Login"
        }.Uri;

        var responseMessage = await client.PostAsync(requestUri, JsonContent.Create(new { LoginName = login, Password = password }));

        
        if ((responseMessage.StatusCode != HttpStatusCode.BadRequest) &&
            !responseMessage.IsSuccessStatusCode)
        {
            responseMessage.EnsureSuccessStatusCode();
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new InvalidAccountException(response.Message);
        }

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await _authService.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, null);
        return RedirectToAction("SignIn");
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        HttpClient client = CreateServiceHttpClient(HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(client.BaseAddress)
        {
            Path = $"/api/v1/Accounts/ChangePassword"
        }.Uri;



        UsersResponse response = null;
        try
        {
            var responseMessage = await client.PostAsync(requestUri, JsonContent.Create(new ChangePasswordRequest()
            {
                OldPassword = model.OldPassword,
                Password = model.Password
            }));

            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();
            responseMessage.EnsureSuccessStatusCode();

            TempData["IsError"] = false;
            TempData["Message"] = response.Message;
            
            await _authService.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, null);
        }
        catch (HttpRequestException ex)
        {
            ViewData["Message"] = response?.Message ?? ex.Message;

            return View();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("SignIn");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
