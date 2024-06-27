using FoodManager.Shared.Options;
using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Shared.Extensions;

public static class AuthenticationWebApplicationBuilderExtensions
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.Authentication));

        var authenticationOptions = new AuthenticationOptions();

        builder.Configuration
           .GetSection(AuthenticationOptions.Authentication)
           .Bind(authenticationOptions);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserIdAccessor, CurrentUserIdAccessor>();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtAuthentication(options =>
            {
                options.Audience = authenticationOptions.Audience;
                options.TokenIssuer = authenticationOptions.TokenIssuer;
                options.TokenExpirySec = authenticationOptions.TokenExpirySec;
                options.SecurityKey = authenticationOptions.SecurityKey;

            })
            .AddApiAuthentication(options =>
            {
                options.Audience = authenticationOptions.ApiAudience;
                options.TokenIssuer = authenticationOptions.TokenIssuer;
                options.TokenExpirySec = authenticationOptions.TokenExpirySec;
                options.SecurityKey = authenticationOptions.SecurityKey;
            });

        builder.Services.AddAuthorization();
    }
}
