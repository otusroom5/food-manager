using FoodManager.Shared.Types;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.WebApi.Contracts;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public partial class ApiKeysController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeysController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }

    [HttpPost("RenewToken")]
    public async Task<ActionResult> RenewApiKey([FromBody] ApiKeyRenewTokenModel model)
    {
        try
        {
            return Ok(new GenericResponse<string>()
            {
               Data = await _apiKeyService.RenewApiKeyAsync(model.OldToken),
               Message = "Success"
            });
        } 
        catch (Exception ex)
        {
            return BadRequest(ResponseBase.Create(ex.Message));
        }
    }

    [HttpGet]
    [Authorize(Roles = UserRole.Administration)]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            IEnumerable<ApiKeyDto> keys = await _apiKeyService.GetKeysAsync();

            return Ok(new GenericResponse<IEnumerable<ApiKeyModel>>()
            {
                Data = keys.Select(x => x.ToModel()),
                Message = "Success"
            });
        } 
        catch (Exception ex) 
        {
            return BadRequest(ResponseBase.Create(ex.Message));
        }
    }

    [HttpPut]
    [Authorize(Roles = UserRole.Administration)]
    public async Task<IActionResult> CreateAsync(ApiKeyCreateModel model) 
    {
        try
        {
            if (!DateTime.TryParseExact(model.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
            {
                return BadRequest(ResponseBase.Create("Ivalid date format 'yyyy-MM-dd'"));
            }

            ApiKeyDto key = await _apiKeyService.CreateApiKeyAsync(expiryDate);

            return Ok(new GenericResponse<ApiKeyModel>()
            {
                Data = key.ToModel(),
                Message = "Success"
            });

        }
        catch (Exception ex)
        {
            return BadRequest(ResponseBase.Create(ex.Message));
        }
    }


    [HttpDelete]
    [Authorize(Roles = UserRole.Administration)]
    public async Task<IActionResult> Delete(string keyId)
    {
        try
        {
            if (!Guid.TryParse(keyId, out Guid id))
            {
                return BadRequest(ResponseBase.Create("Invalid Api key"));
            }

            await _apiKeyService.DeleteAsync(id);

            return Ok(ResponseBase.Create("Success"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseBase.Create(ex.Message));
        }
    }

}
