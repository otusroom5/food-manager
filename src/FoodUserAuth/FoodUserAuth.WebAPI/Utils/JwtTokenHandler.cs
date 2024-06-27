using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using FoodUserAuth.DataAccess.Types;
using FoodManager.Shared.Auth.Utils;
using Microsoft.Extensions.Options;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodManager.Shared.Options;

namespace FoodUserAuth.WebApi.Utils;

internal class JwtTokenHandler: ITokenHandler
{
    private const int ExpiryTokenTimeSec = 300;
    private readonly AuthenticationOptions _options;

    public JwtTokenHandler(IOptions<AuthenticationOptions> options)
    {
        _options = options.Value;
    }

    public ApiKeyData ExtractApiKeyData(string token)
    {
        try
        {
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            ApiKeyClaimsHelper apiKeyClaims = ApiKeyClaimsHelper.Parce(jwtToken.Claims);

            return new()
            {
                KeyId = apiKeyClaims.KeyId,
                UserId = apiKeyClaims.UserId,
                ValidTo = jwtToken.ValidTo,
            };
        }
        catch
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

        return Generate(claims, _options.TokenExpirySec, _options.Audience);
    }

    public string GenerateApiToken(Guid apiKeyId, Guid userId)
    {
        ApiKeyClaimsHelper claims = new ApiKeyClaimsHelper()
        {
            KeyId = apiKeyId,
            UserId = userId
        };

        return Generate(claims.ToList(), ExpiryTokenTimeSec, _options.ApiAudience);
    }

    private string Generate(IEnumerable<Claim> claims, int expiryTokenTimeSec, string audience)
    {
        var jwtSecToken = new JwtSecurityToken(
                issuer: _options.TokenIssuer,
                claims: claims,
                audience: audience,
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
