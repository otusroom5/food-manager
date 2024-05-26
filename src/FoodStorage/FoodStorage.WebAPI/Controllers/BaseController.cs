using FoodStorage.Domain.Entities;
using FoodStorage.WebApi.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodStorage.WebApi.Controllers;

public abstract class BaseController : ControllerBase
{
    protected UserId UserId => GetCurrentUserId();

    private UserId GetCurrentUserId()
    {
        if (!Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value, out Guid userGuid))
        {
            throw new WebApiException("Invalid authorization");
        }

        return UserId.FromGuid(userGuid);
    }
}
