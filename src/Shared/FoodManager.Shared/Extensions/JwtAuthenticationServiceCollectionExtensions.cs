using FoodManager.Shared.Auth.Options;
using FoodManager.Shared.Auth.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodManager.Shared.Auth.Extensions;

public static class JwtAuthenticationServiceCollectionExtensions
{
    /// <summary>
    /// This method configures authentication with Jwt token
    /// </summary>
    /// <param name="authenticationOptions"></param>
    /// <returns>AuthenticationBuilder</returns>
    /// 
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, Action<JwtAuthenticationOptions> action)
    {
        var options = new JwtAuthenticationOptions();
        action?.Invoke(options);

        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
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
