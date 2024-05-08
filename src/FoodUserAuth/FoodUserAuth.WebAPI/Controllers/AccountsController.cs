using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FoodUserAuth.WebApi.Utils;
using FoodUserAuth.WebApi.Contracts;
using FoodUserAuth.WebApi.Models;
using Microsoft.Extensions.Logging;
using System;
using FoodUserAuth.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FoodManager.Shared.Auth.Options;
using FoodManager.Shared.Types;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly JwtAuthenticationOptions _options;
    private readonly IUsersService _userService;

    public AccountsController(ILogger<AccountsController> logger,
        IUsersService userService,
        IOptions<JwtAuthenticationOptions> options)
    {
        _logger = logger;
        _options = options.Value;
        _userService = userService;
    }

    /// <summary>
    /// Verify user and return response jwt token
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns>Token, Role or error in message property</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/v1/Accounts/Login
    ///     {
    ///        "UserName": "test",
    ///        "HashedPassword": "hashed_password",
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Token is generated</response>
    /// <response code="400">If the user is not valid</response>
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginModel userModel)
    {
        _logger.LogTrace($"Attempt login {userModel.LoginName}");
        try
        {
            var user = await _userService.VerifyAndGetUserIfSuccessAsync(userModel.LoginName, userModel.Password);
            
            string token = JwtTokenUtils.GenerateToken(_options, user.LoginName, user.Role);

            _logger.LogDebug($"Generated token: {token}");
            
            return Ok(new LoginActionResponse()
            {
                Token = token,
                Role = user.Role.ToString(),
                Message = "Success"
            });
        } 
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(new LoginActionResponse()
            {
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Change password of user by username
    /// </summary>
    /// <param name="userModel"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/v1/Accounts/ChangePassword
    ///     {
    ///        "UserName": "test",
    ///        "HashedPassword": "hashed_password",
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Password is changed</response>
    /// <response code="400">If password is not changed then return error</response>

    [Authorize(Roles = UserRole.Administration)]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(UserLoginModel userModel)
    {
        try
        {
            await _userService.ChangePasswordAsync(userModel.LoginName, userModel.Password);
            return Ok();
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(new MessageResponse()
            {
                Message = ex.Message
            });
        }
    }
}
