using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.WebApi.Contracts;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]

[Authorize(AuthenticationSchemes = "ApiKey")]
public class UserContactsController : ControllerBase
{
    private readonly IUserContactsService _userContactsService;
    
    public UserContactsController(IUserContactsService userContactsService)
    {
        _userContactsService = userContactsService;
    }

    [HttpPut("GetAllForRole")]
    public async Task<IActionResult> GetAllForRole(string role)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return BadRequest(ResponseBase.Create("Role is not defined"));
            }

            DataAccess.Types.UserRole userRole = Enum.Parse<DataAccess.Types.UserRole>(role);

            if (userRole == DataAccess.Types.UserRole.Administrator)
            {
                return BadRequest(ResponseBase.Create($"Invalid User role. Accessible: {DataAccess.Types.UserRole.Cooker.ToString()}, {DataAccess.Types.UserRole.Manager.ToString()}"));
            }

            var userContacts = await _userContactsService.GetAllForRoleAsync(userRole);

            return Ok(new GenericResponse<IEnumerable<UserContactModel>>()
            {
                Data = userContacts.Select(f => f.ToModel()),
                Message = "Success"
            });
        } 
        catch (Exception ex) 
        { 
            return BadRequest(ResponseBase.Create(ex.Message));
        }
    }
}
