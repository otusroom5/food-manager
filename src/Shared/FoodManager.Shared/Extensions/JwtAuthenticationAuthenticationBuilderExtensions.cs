using FoodManager.Shared.Auth.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodManager.Shared.Auth.Extensions;

public static class JwtAuthenticationAuthenticationBuilderExtensions
{
    /// <summary>
    /// This method configures authentication with Jwt token
    /// </summary>
    /// <param name="authenticationOptions"></param>
    /// <returns>AuthenticationBuilder</returns>
    /// 
    public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder builder, Action<Options.AuthenticationOptions> action)
    {
        var options = new Options.AuthenticationOptions();
        action?.Invoke(options);

        return builder.AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = options.TokenIssuer,
                        ValidateAudience = true,
                        ValidAudience = options.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = SecurityKeyUtils.CreateSymmetricSecurityKey(options.SecurityKey),
                        ValidateIssuerSigningKey = true
                    };
                });
    }
}
