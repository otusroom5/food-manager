using FoodManager.Shared.Types;
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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FoodUserAuth.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ApiKeysController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;
    private readonly ILogger<ApiKeysController> _logger;

    public ApiKeysController(IApiKeyService apiKeyService, ILogger<ApiKeysController> logger)
    {
        _apiKeyService = apiKeyService;
        _logger = logger;
    }

    [HttpPost("RenewToken")]
    public async Task<ActionResult> RenewApiKey([FromBody] ApiKeyRenewTokenRequest request)
    {
        try
        {
            string newToken = await _apiKeyService.RenewApiKeyAsync(request.OldToken);

            _logger.LogDebug("New token is generated");

            return Ok(new GenericResponse<string>()
            {
               Data = newToken,
               Message = "Success"
            });
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure());
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
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles = UserRole.Administration)]
    public async Task<IActionResult> CreateAsync(ApiKeyCreateRequest request) 
    {
        try
        {
            if (!DateTime.TryParseExact(request.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
            {
                return BadRequest(ResponseBase.Create("Ivalid date format 'yyyy-MM-dd'"));
            }

            ApiKeyDto key = await _apiKeyService.CreateApiKeyAsync(expiryDate);


            _logger.LogInformation("New Api key is added {Id}", key.Id);

            return Ok(new GenericResponse<ApiKeyModel>()
            {
                Data = key.ToModel(),
                Message = "Success"
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure(ex));
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
            _logger.LogError(ex, ex.Message);

            return BadRequest(ResponseBase.CreateFailure(ex));
        }
    }

}
