using FoodManager.Shared.Auth.Extensions;
using FoodManager.Shared.Auth.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Shared.Extensions;

public static class AuthenticationWebApplicationBuilderExtensions
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtAuthenticationOptions>(builder.Configuration.GetSection(JwtAuthenticationOptions.Authentication));
        
        builder.Services.AddJwtAuthentication(options =>
        {
            options.LoadFromConfiguration(builder.Configuration);
        });

        builder.Services.AddAuthorization();
    }
}
