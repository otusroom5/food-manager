using FoodManager.WebUI.Options;

namespace FoodManager.WebUI.Extensions;

public static class AuthenticationOptionsExtensions
{
    public static void LoadFromConfiguration(this AuthenticationOptions options, IConfiguration configuration)
    {
        configuration.GetSection(AuthenticationOptions.Authentication)
            .Bind(options);
    }
}
