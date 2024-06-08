using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using FoodManager.Shared.Utils.Interfaces;

namespace FoodManager.Shared.Utils.Implementations;

public class CurrentUserIdAccessor : ICurrentUserIdAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserIdAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        string nameIndetifier = _httpContextAccessor
            .HttpContext
            .User.Claims.First(f => f.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        return Guid.TryParse(nameIndetifier, out Guid userId) ? userId : Guid.Empty;
    }
}
