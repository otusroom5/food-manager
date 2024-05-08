using FoodManager.Shared.Auth.Options;
using Microsoft.Extensions.Configuration;

namespace FoodManager.Shared.Extensions;

public static class JwtAuthenticationOptionsExtensions
{
    public static void LoadFromConfiguration(this JwtAuthenticationOptions options, IConfiguration configuration)
    {        
        configuration
            .GetSection(JwtAuthenticationOptions.Authentication)
            .Bind(options);
    }
}
