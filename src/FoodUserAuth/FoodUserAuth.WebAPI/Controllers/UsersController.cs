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
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Contracts.Requests;

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
    public async Task<ActionResult<IEnumerable<UserModel>>> Get()
    {
        try
        {
            var items = await _usersService.GetAllAsync();

            return Ok(new GenericResponse<UserModel[]>()
            {
                Data = items.Select(f => f.ToModel()).ToArray(),
                Message = "Success"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
        }
    }


    [HttpGet("GetById")]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetById(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogInformation("Id is not valid");
                return BadRequest(ResponseBase.CreateFailure());
            }

            if (!Guid.TryParse(id, out Guid employeeId))
            {
                _logger.LogInformation("Id is not Guid");
                return BadRequest(ResponseBase.CreateFailure());
            }

            UserDto foundUser = await _usersService.GetAsync(employeeId);

            return Ok(new GenericResponse<UserModel[]>()
            {
                Data = new UserModel[] { foundUser.ToModel() },
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
    /// Create a new user
    /// </summary>
    /// <param name="item">User</param>
    /// <returns>Return new UserId and password like string</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPut]
    public async Task<ActionResult> Create([FromBody] UserCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("UserCreateModel is not valid");

            return BadRequest(ResponseBase.CreateFailure());
        }

        try
        {
            var result = await _usersService.CreateAsync(request.ToDto());

            _logger.LogDebug("Create user ({Id}) was create", result.User.Id);

            return Ok(new GenericResponse<UserCreateModel>()
            {
                Data = new UserCreateModel()
                {
                    UserId = result.User.Id,
                    Password = result.Password
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
    /// Update user
    /// </summary>
    /// <param name="item"></param>
    /// <returns>List of users</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPost]
    public async Task<ActionResult> Update([FromBody] UserUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("UserUpdateModel is not valid");
            return BadRequest(ResponseBase.Create("UserUpdateModel is not valid"));
        }

        try
        {
            await _usersService.UpdateAsync(request.ToDto());

            _logger.LogDebug("User was updated");
            return Ok(ResponseBase.CreateSuccess());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
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
    public async Task<ActionResult> Disable(string userId)
    {
        try
        {
            await _usersService.DisableAsync(Guid.Parse(userId));

            _logger.LogDebug("User ({Id}) was disabled", userId);

            return Ok(ResponseBase.CreateSuccess());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
        }
    }
}
