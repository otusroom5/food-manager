using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.WebApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FoodUserAuth.BusinessLogic.Interfaces;
using System;
using FoodUserAuth.WebApi.Contracts;
using System.Linq;
using System.Threading.Tasks;
using FoodManager.Shared.Types;
using System.Security.Claims;
using FoodUserAuth.BusinessLogic.Services;
using FoodUserAuth.BusinessLogic.Dto;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace FoodUserAuth.WebApi.Controllers;

[Authorize(Roles = UserRole.Administration)]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _usersService;

    public UsersController(ILogger<UsersController> logger,
        IUsersService usersService)
    {
        _usersService = usersService;
        _logger = logger;
    }

    /// <summary>
    /// Return list of all user
    /// </summary>
    /// <param name="item"></param>
    /// <returns>List of users</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> Get(string id = "")
    {
        if (!string.IsNullOrWhiteSpace(id)) 
        {
            if (Guid.TryParse(id, out Guid employeeId))
            {
                UserDto foundUser = await _usersService.GetAsync(employeeId);
                return new UserModel[] { foundUser.ToModel() };
            }

            throw new ArgumentException(nameof(id));
        }

        var items = await _usersService.GetAllAsync();
        return Ok(new GenericResponse<UserModel[]>()
        {
            Data = items.Select(f => f.ToModel()).ToArray(),
            Message = "Ok"
        });
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="item">User</param>
    /// <returns>Return new UserId and password like string</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPut]
    public async Task<ActionResult> Create([FromBody] UserCreateModel model)
    {
        try
        {
            var result = await _usersService.CreateUserAsync(model.ToDto());

            _logger.LogDebug("Create user ({Id}) was create", result.User.Id);

            return Ok(new GenericResponse<UserCreatedModel>()
            {
                Data = new UserCreatedModel()
                {
                    UserId = result.User.Id,
                    Password = result.Password
                },
                Message = "Ok"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.Create(ex));
        }
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="item"></param>
    /// <returns>List of users</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPost]
    public async Task<ActionResult> Update([FromBody] UserUpdateModel model)
    {
        try
        {
            await _usersService.UpdateUserAsync(model.ToDto());

            _logger.LogDebug("User was updated");
            return Ok(ResponseBase.Create("Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.Create(ex));
        }
    }

    /// <summary>
    /// Disable User
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Message</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpDelete]
    public async Task<ActionResult> Delete(UserDeleteModel item)
    {
        try
        {
            await _usersService.DisableUserAsync(Guid.Parse(item.Id));

            _logger.LogDebug("User ({Id}) was disabled", item.Id);
            return Ok(ResponseBase.Create("Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.Create(ex));
        }
    }
}
