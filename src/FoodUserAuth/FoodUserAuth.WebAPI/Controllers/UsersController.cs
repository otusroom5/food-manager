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

namespace FoodUserAuth.WebApi.Controllers;

[Authorize(Roles = UserRoleExtension.AdministrationRole)]
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
    public ActionResult<IEnumerable<UserModel>> GetAll()
    {
        return Ok(_usersService.GetAll().Select(f => f.ToModel()));
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="item">User</param>
    /// <returns>Return new UserId and password like string</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPut]
    public ActionResult Create(UserCreateModel model)
    {
        try
        {
            var result = _usersService.CreateUser(model.ToDto());

            return Ok(new CreateUserResponse()
            {
                UserId = result.User.Id,
                Password = result.Password
            });
        }
        catch (Exception ex)
        {
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
    public ActionResult Update(UserModel model)
    {
        try
        {
            _usersService.UpdateUser(model.ToDto());

            return Ok(new MessageResponse()
            {
                Message = "Success"
            });
        }
        catch (Exception ex)
        {
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
    public ActionResult Delete(UserDeleteModel item)
    {
        try
        {
            _usersService.DisableUser(Guid.Parse(item.Id));

            return Ok(new MessageResponse()
            {
                Message = "Success"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new MessageResponse()
            {
                Message = ex.Message
            });
        }
    }
}
