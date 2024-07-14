using Microsoft.AspNetCore.Mvc;
using FoodUserAuth.WebApi.Utils;
using FoodUserAuth.WebApi.Contracts;
using FoodUserAuth.WebApi.Models;
using Microsoft.Extensions.Logging;
using System;
using FoodUserAuth.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FoodManager.Shared.Types;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Contracts.Requests;
using FoodUserAuth.BusinessLogic.Services;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IUsersService _userService;
    private readonly IUserVerifier _userVerifier;
    private readonly ITokenHandler _tokenService;
    private readonly IUserPasswordChanger _userPasswordChanger;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public AccountsController(ILogger<AccountsController> logger,
        IUsersService userService,
        IUserVerifier userVerifier,
        ICurrentUserAccessor currentUserAccessor,
        IUserPasswordChanger userPasswordChanger,
        ITokenHandler tokenService)
    {
        _logger = logger;
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userPasswordChanger = userPasswordChanger ?? throw new ArgumentNullException(nameof(userPasswordChanger));
        _userVerifier = userVerifier ?? throw new ArgumentNullException(nameof(userVerifier));
        _currentUserAccessor = currentUserAccessor ?? throw new ArgumentNullException(nameof(currentUserAccessor));
    }

    /// <summary>
    /// Verify user and return response jwt token
    /// </summary>
    /// <param name="request"></param>
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
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("UserLoginModel is not valid");
            BadRequest(ResponseBase.Create("Model is not valid"));
        }

        _logger.LogDebug("Attempt to login {LoginName}", request.LoginName);
        try
        {
            if (!await _userVerifier.VerifyAsync(request.LoginName, request.Password))
            {
                _logger.LogInformation("User not found");
                return BadRequest(ResponseBase.Create("User not found"));
            }

            UserDto user = await _userService.FindByLoginNameAsync(request.LoginName);

            _logger.LogInformation("User ({1}) is accepted", request.LoginName);

            string token = _tokenService.Generate(user.LoginName, user.Id, user.Role);

            _logger.LogDebug("Generated token: {1}", token);
            return Ok(new GenericResponse<AuthenticationModel>()
            {
                Data = new AuthenticationModel()
                {
                    UserId = user.Id.ToString(),
                    Token = token,
                    Role = user.Role.ToString(),
                },

                Message = "Success"
            });
        } 
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
        }
    }

    /// <summary>
    /// Change password of user by username
    /// </summary>
    /// <param name="request"></param>
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

    [Authorize(Roles = $"{UserRole.Administration}, {UserRole.Manager}, {UserRole.Cooker}")]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(UserChangePasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("UserChangePasswordModel is not valid");
            BadRequest(ResponseBase.Create("Model is not valid"));
        }

        try
        {
            User currentUser = await _currentUserAccessor.GetCurrentUserAsync();
            await _userPasswordChanger.ChangeAsync(currentUser, request.OldPassword, request.Password);
            
            _logger.LogInformation("Password is changed");
            
            return Ok(ResponseBase.Create("Success"));
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure());
        }
    }

    [Authorize(Roles = UserRole.Administration)]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ResetPasswordModel is not valid");

            return BadRequest(ResponseBase.Create("Model is not valid"));
        }

        try
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                _logger.LogInformation("Id identifier is not valid");

                return BadRequest(ResponseBase.Create("Id identifier is not valid"));
            }

            string newPassword = await _userPasswordChanger.ResetAsync(userId);

            _logger.LogWarning("Password is reseted for {1}", userId);

            return Ok(new GenericResponse<ResetPasswordModel>()
            { 
                Data = new ResetPasswordModel()
                {
                    Password = newPassword
                },
                Message = "Success"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure());
        }
    }
}
