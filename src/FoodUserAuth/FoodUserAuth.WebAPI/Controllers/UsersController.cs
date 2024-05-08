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
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
    {
        var items = await _usersService.GetAllAsync();
        return Ok(items.Select(f => f.ToModel()));
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="item">User</param>
    /// <returns>Return new UserId and password like string</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPut]
    public async Task<ActionResult> Create(UserCreateModel model)
    {
        try
        {
            var result = await _usersService.CreateUserAsync(model.ToDto());

            _logger.LogDebug($"Create user ({result.User.Id}) was create");

            return Ok(new CreateUserResponse()
            {
                UserId = result.User.Id,
                Password = result.Password
            });
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

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="item"></param>
    /// <returns>List of users</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPost]
    public async Task<ActionResult> Update(UserModel model)
    {
        try
        {
            await _usersService.UpdateUserAsync(model.ToDto());

            _logger.LogDebug("User was updated");
            return Ok(new MessageResponse()
            {
                Message = "Success"
            });
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

            _logger.LogDebug($"User ({item.Id}) was disabled");
            return Ok(new MessageResponse()
            {
                Message = "Success"
            });
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
