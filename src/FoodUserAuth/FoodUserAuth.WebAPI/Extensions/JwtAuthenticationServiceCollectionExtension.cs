using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FoodUserAuth.WebApi.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FoodUserAuth.WebApi.Extensions;

public static class JwtAuthenticationServiceCollectionExtension
{
    /// <summary>
    /// This method configures authentication with Jwt token
    /// </summary>
    /// <param name="authenticationOptions"></param>
    /// <returns>AuthenticationBuilder</returns>
    /// 
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, Options.AuthenticationOptions authenticationOptions)
    {
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authenticationOptions.TokenIssuer,
                        ValidateAudience = true,
                        ValidAudience = authenticationOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = SecurityKeyUtils.CreateSymmetricSecurityKey(authenticationOptions.SecurityKey),
                        ValidateIssuerSigningKey = true
                    };
                });
    }
}
