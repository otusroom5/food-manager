using FoodManager.WebUI.Areas.Administrator.Contracts.Responses;
using FoodManager.WebUI.Areas.Administrator.Models;
using FoodManager.WebUI.Contracts;
using FoodManager.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.WebUI.Areas.Administrator.Controllers;

[Area("Administrator")]
[Authorize(Roles = "Administrator")]
public class AdministratorController : Abstractions.ControllerBase
{
    private static readonly string UsersRESTEndPoint = "/api/v1/Users";
    private readonly ILogger<AdministratorController> _logger;

    public AdministratorController(IHttpClientFactory httpClientFactory, ILogger<AdministratorController> logger) : base(httpClientFactory)
    {
        _logger = logger;
    }

    [Route("{area}")]
    [Route("{area}/{controller}")]
    public async Task<IActionResult> Index()
    {
        var httpClient = CreateServiceHttpClient(serviceName: HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UsersRESTEndPoint
        }.Uri;


        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
            responseMessage.EnsureSuccessStatusCode();
            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();
            
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

    [Route("{area}/{controller}/{action}/{id}")]
    public async Task<IActionResult> Disable(UserDeleteModel model)
    {
        var httpClient = CreateServiceHttpClient(serviceName: HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path =  UsersRESTEndPoint,
            Query = $"Id={model.Id}"
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
        var httpClient = CreateServiceHttpClient(serviceName: HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UsersRESTEndPoint
        }.Uri;

        UserCreatedResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsync(requestUri, JsonContent.Create(model));
            responseMessage.EnsureSuccessStatusCode();
            response = await responseMessage.Content.ReadFromJsonAsync<UserCreatedResponse>();

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
    public async Task<IActionResult> Update(string id)
    {
        if (!Guid.TryParse(id, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateServiceHttpClient(serviceName: HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UsersRESTEndPoint
        }.Uri;

        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
            responseMessage.EnsureSuccessStatusCode();
            response = await responseMessage.Content.ReadFromJsonAsync<UsersResponse>();

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
                User = response?.Data.FirstOrDefault(f => f.Id.Equals(id))?.ToModel()
            });
    }


    [HttpPost]
    [Route("{area}/{controller}/{action}")]
    public async Task<IActionResult> Update(UserUpdateModel model)
    {
        if (!Guid.TryParse(model.Id, out _))
        {
            throw new ArgumentException("Id is not correct");
        }

        var httpClient = CreateServiceHttpClient(serviceName: HttpClientWebApplicationExtensions.AuthServiceName);

        Uri requestUri = new UriBuilder(httpClient.BaseAddress)
        {
            Path = UsersRESTEndPoint
        }.Uri;

        UsersResponse response = null;
        try
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, JsonContent.Create(model));

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
}
