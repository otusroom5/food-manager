using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodManager.WebUI.Extensions;

public static class CookieAuthenticationServiceCollectionExtensions
{
    private const string DefaultClaimIssuer = "FoodManager.WebUI";
    private const string DefaultCookieName = "FoodManager";
    private const int DefaultExpiryTimeMin = 20;

    public static AuthenticationBuilder AddCookieAuthentication(this IServiceCollection services, 
        Action<Options.AuthenticationOptions> action, string returnUrlParam)
    {
        var options = new Options.AuthenticationOptions();
        action?.Invoke(options);

        return services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(authOptions =>
                {
                    authOptions.ClaimsIssuer = options.ClaimsIssuer ?? DefaultClaimIssuer;
                    authOptions.ReturnUrlParameter = returnUrlParam;
                    authOptions.Cookie.Name = options.CookieName ?? DefaultCookieName;
                    authOptions.ExpireTimeSpan = TimeSpan.FromMinutes(options.ExpireTimeMin == default ? DefaultExpiryTimeMin : options.ExpireTimeMin);
                    authOptions.SlidingExpiration = true;
                    authOptions.AccessDeniedPath = "/Forbidden/";
                });
    }
}
