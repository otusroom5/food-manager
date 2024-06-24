using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodUserAuth.WebApi.Utils;

public class ApiKeyClaimsHelper 
{
    public Guid KeyId { get; set; }
    public Guid UserId { get; set; }

    public IEnumerable<Claim> ToList()
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
            new Claim(ClaimTypes.UserData, KeyId.ToString())
        };
    }

    public static ApiKeyClaimsHelper Parce(IEnumerable<Claim> claims)
    {
        string nameIdentifier = claims
                       .FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty;

        Guid userId = Guid.TryParse(nameIdentifier, out userId) ? userId : Guid.Empty;

        string userData = claims
                .FirstOrDefault(f => f.Type.Equals(ClaimTypes.UserData))?.Value ?? string.Empty;

        Guid keyId = Guid.TryParse(userData, out keyId) ? keyId : Guid.Empty;

        return new()
        {
            KeyId = keyId,
            UserId = userId
        };
    }
}
