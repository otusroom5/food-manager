using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.WebApi.Contracts;
using FoodUserAuth.WebApi.Contracts.Requests;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]

[Authorize(AuthenticationSchemes = "ApiKey")]
public class UserContactsController : ControllerBase
{
    private readonly ILogger<UserContactsController> _logger;
    private readonly IUserContactsService _userContactsService;
    
    public UserContactsController(IUserContactsService userContactsService, ILogger<UserContactsController> logger)
    {
        _logger = logger;
        _userContactsService = userContactsService;
    }

    [HttpGet("GetAllForRole")]
    public async Task<IActionResult> GetAllForRole(string role)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                _logger.LogInformation("Role is not defined");
                return BadRequest(ResponseBase.Create("Role is not defined"));
            }

            DataAccess.Types.UserRole userRole = Enum.Parse<DataAccess.Types.UserRole>(role);

            if (userRole == DataAccess.Types.UserRole.Administrator)
            {
                return BadRequest(ResponseBase.Create("Invalid User role"));
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
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
        }
    }

    [HttpGet("GetContact")]
    public async Task<IActionResult> GetContact([FromQuery] GetContactRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Contact))
            {
                _logger.LogInformation("Contact is not defined");
                return BadRequest(ResponseBase.Create("Contact is not defined"));
            }

            UserContactDto userContact = await _userContactsService.FindContact(request.ContactType, request.Contact);

            return Ok(new GenericResponse<UserContactModel>()
            {
                Data = userContact.ToModel(),
                Message = userContact != null ? "Success" : "Fail"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ResponseBase.CreateFailure());
        }
    }
}
