using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using FoodUserAuth.DataAccess.Types;
using FoodManager.Shared.Auth.Options;
using FoodManager.Shared.Auth.Utils;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Linq;
using FoodUserAuth.BusinessLogic.Exceptions;

namespace FoodUserAuth.WebApi.Utils;

public class JwtTokenHandler: ITokenHandler
{
    private const int ExpiryTokenTimeSec = 300;
    private readonly AuthenticationOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenHandler(IOptions<AuthenticationOptions> options, 
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options.Value;
    }

    public (Guid ApiKeyId, DateTime ValidTo) ExtractApiKeyData(string token)
    {
        try
        {
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string userData = jwtToken.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.UserData))?.Value ?? string.Empty;
            return (ApiKeyId: Guid.Parse(userData), ValidTo: jwtToken.ValidTo);
        } catch
        {
            throw new InvalidApiKeyException();
        }
    }

    public string Generate(string loginName, Guid id, UserRole role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, loginName),
            new Claim(ClaimTypes.Role, role.ToString()),
        };

        return Generate(claims, _options.TokenExpirySec);
    }

    public string GenerateApiToken(Guid apiKeyId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, GetCurrentUserId()),
            new Claim(ClaimTypes.UserData, apiKeyId.ToString())
        };

        return Generate(claims, ExpiryTokenTimeSec);
    }

    private string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext.User.Claims.First(f => f.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }

    private string Generate(IEnumerable<Claim> claims, int expiryTokenTimeSec)
    {
        var jwtSecToken = new JwtSecurityToken(
                issuer: _options.TokenIssuer,
                claims: claims,
                audience: _options.Audience,
                expires: CalculateExpiryTime(expiryTokenTimeSec),
                signingCredentials: CreateCredentials(_options.SecurityKey));

        return new JwtSecurityTokenHandler().WriteToken(jwtSecToken);
    }

    private DateTime CalculateExpiryTime(int seconds)
    {
        return DateTime.UtcNow.Add(TimeSpan.FromSeconds(seconds));
    }

    private static SigningCredentials CreateCredentials(string key)
    {
        return new SigningCredentials(SecurityKeyUtils.CreateSymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }

}
