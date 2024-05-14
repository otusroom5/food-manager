using FoodManager.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FoodManager.WebUI.Services.Interfaces;
using System.Diagnostics;
using FoodManager.WebUI.Exceptions;

namespace FoodManager.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authService;

    public AccountController(IAuthenticationService authService,
        ILogger<AccountController> logger,
        IAccountService accountService)
    {
        _logger = logger;
        _authService = authService;
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            var acceptedData = await _accountService.LogInAsync(model.Login, model.Password);

            _logger.LogInformation("Account ({Login}) is accepted. Assigned following role: {Role}", model.Login, acceptedData.Role);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Login),
                new Claim(ClaimTypes.Role, acceptedData.Role),
                new Claim(ClaimTypes.UserData, acceptedData.Token)
            };

            var claimsIdentity = new ClaimsIdentity(
              claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "Account/Login"
            };

            await _authService.SignInAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              authProperties);

            return RedirectToAction("", acceptedData.Role);
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

    [HttpPost]
    public async Task<IActionResult> Logout(LoginModel model)
    {
        throw new NotImplementedException();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
