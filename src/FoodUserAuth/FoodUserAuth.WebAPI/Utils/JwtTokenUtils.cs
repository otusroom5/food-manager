using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using FoodUserAuth.DataAccess.Types;
using FoodManager.Shared.Auth.Options;
using FoodManager.Shared.Auth.Utils;

namespace FoodUserAuth.WebApi.Utils;

internal static class JwtTokenUtils
{
    /// <summary>
    /// This method generates Jwt token with username, role in claims.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="userName"></param>
    /// <param name="role"></param>
    /// <returns>string</returns>
    public static string GenerateToken(JwtAuthenticationOptions options, string loginName, Guid id, UserRole role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, loginName),
            new Claim(ClaimTypes.Role, role.ToString()),
        };

        var jwtSecToken = new JwtSecurityToken(
                issuer: options.TokenIssuer,
                audience: options.Audience,
                claims: claims,
                expires: CalculateExpiryTime(options.TokenExpirySec),
                signingCredentials: CreateCredentials(options.SecurityKey));

        return new JwtSecurityTokenHandler().WriteToken(jwtSecToken);
    }

    private static DateTime CalculateExpiryTime(int seconds)
    {
        return DateTime.UtcNow.Add(TimeSpan.FromSeconds(seconds));
    }

    private static SigningCredentials CreateCredentials(string key)
    {
        return new SigningCredentials(SecurityKeyUtils.CreateSymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }

}
