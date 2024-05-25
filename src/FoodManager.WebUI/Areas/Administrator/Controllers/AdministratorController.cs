using FoodManager.WebUI.Areas.Administrator.Contracts.Responses;
using FoodManager.WebUI.Areas.Administrator.Models;
using FoodManager.WebUI.Contracts;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Administrator.Controllers;

[Area("Administrator")]
[Authorize(Roles = "Administrator")]
public sealed class AdministratorController : Abstractions.ControllerBase
{
    private static readonly string UsersApiUrl = "/api/v1/Users";
    private static readonly string ResetPasswordRESTEndPoint = "/api/v1/Accounts/ResetPassword";
    private readonly ILogger<AdministratorController> _logger;

    public AdministratorController(IHttpClientFactory httpClientFactory, ILogger<AdministratorController> logger) : base(httpClientFactory)
    {
        _logger = logger;
    }

    [Route("{area}")]
    [Route("{area}/{controller}")]
    public async Task<IActionResult> Index()
    {
        var httpClient = CreateUserAuthServiceClient();

        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(UsersApiUrl);

            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();
            responseMessage.EnsureSuccessStatusCode();
            
            if (!TempData.Keys.Contains("IsError"))
            {
                TempData["IsError"] = false;
                TempData["Message"] = response.Message;
            }
        }
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        } 
        catch (Exception ex) 
        {
            throw ex;
        }
       
        return View(
            new UserIndexModel()
            {
                Users = response?.Data.Select(f => f.ToModel()).ToArray()
            });
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Disable(string userId)
    {
        var httpClient = CreateUserAuthServiceClient();

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path =  UsersApiUrl,
            Query = QueryString.Create("UserId", userId).Value
        }.Uri;

        ResponseBase response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);
            
            response = await responseMessage.Content.ReadFromJsonAsync<ResponseBase>();
            responseMessage.EnsureSuccessStatusCode();

            TempData["IsError"] = false;
            TempData["Message"] = response.Message;
        } 
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex) 
        {
            throw ex;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Create(UserCreateModel model)
    {
        var httpClient = CreateUserAuthServiceClient();

        UserCreatedResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsync(UsersApiUrl, JsonContent.Create(model));

            response = await responseMessage.Content.ReadFromJsonAsync<UserCreatedResponse>();
            responseMessage.EnsureSuccessStatusCode();
            
            TempData["IsError"] = false;
            TempData["Message"] = $"User is created. Generated password: {response.Data.Password}";
        }
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Update(string userId)
    {
        if (!Guid.TryParse(userId, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateUserAuthServiceClient();

        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(UsersApiUrl);

            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();
            responseMessage.EnsureSuccessStatusCode();
            
            if (!TempData.Keys.Contains("IsError"))
            {
                TempData["IsError"] = false;
                TempData["Message"] = response.Message;
            }
        }
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return View("Index",
            new UserIndexModel()
            {
                Users = response?.Data.Select(f => f.ToModel()).ToArray(),
                User = response?.Data.FirstOrDefault(f => f.UserId.Equals(userId))?.ToModel()
            });
    }


    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Update(UserUpdateModel model)
    {
        if (!Guid.TryParse(model.UserId, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateUserAuthServiceClient();

        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsync(UsersApiUrl, JsonContent.Create(model));

            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();
            responseMessage.EnsureSuccessStatusCode();
           

            if (!TempData.Keys.Contains("IsError"))
            {
                TempData["IsError"] = false;
                TempData["Message"] = response.Message;
            }
        }
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> ResetPassword(string userId)
    {
        if (!Guid.TryParse(userId, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateUserAuthServiceClient();

        UserResetPasswordResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsync(ResetPasswordRESTEndPoint, JsonContent.Create(new
            {
                UserId = userId
            }));

            response = await responseMessage.Content.ReadFromJsonAsync<UserResetPasswordResponse>();
            responseMessage.EnsureSuccessStatusCode();

            TempData["IsError"] = false;
            TempData["Message"] = $"Password is reseted. Password: {response.Data.Password}";
        }
        catch (HttpRequestException ex)
        {
            TempData["IsError"] = true;
            TempData["Message"] = response?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return RedirectToAction("Index");
    }
}
