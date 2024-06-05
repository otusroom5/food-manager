using FoodManager.Shared.Auth.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FoodManager.Shared.Extensions;

public static class ApiAuthenticationAuthenticationBuilderExtensions
{
    public readonly static string ApiKeyAuthentication = "ApiKey";

    public static AuthenticationBuilder AddApiAuthentication(this AuthenticationBuilder services, Action<Options.AuthenticationOptions> action)
    {
        var authenticationOptions = new Options.AuthenticationOptions();
        action?.Invoke(authenticationOptions);

        return services
            .AddScheme<ApiAuthenticationSchemeOptions, ApiAuthenticationHandler>(authenticationScheme: ApiKeyAuthentication, options => 
            {
                options.TokenIssuer = authenticationOptions.TokenIssuer;
                options.Audience = authenticationOptions.Audience;
                options.SecurityKey = authenticationOptions.SecurityKey;
             });
    }

    public class ApiAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string TokenIssuer { get; set; }
        public string SecurityKey { get; set; }
        public string Audience { get; set; }
    }

    public class ApiAuthenticationHandler : AuthenticationHandler<ApiAuthenticationSchemeOptions>
    {
        public ApiAuthenticationHandler(IOptionsMonitor<ApiAuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (TryToExtractApiKeyAuthorizationHeaderValue(Request.Headers.Authorization, out var headerVaue))
            {

                TokenValidationResult validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(headerVaue.Parameter, new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Options.TokenIssuer,
                    ValidateAudience = true,
                    ValidAudience = Options.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = SecurityKeyUtils.CreateSymmetricSecurityKey(Options.SecurityKey),
                    ValidateIssuerSigningKey = true
                });


                if (!validationResult.IsValid)
                {
                    return AuthenticateResult.Fail("Invalid Api Key");
                }

                var claims = new List<Claim>(validationResult.Claims.Select(f => new Claim(f.Key, f.Value.ToString())));
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, ApiKeyAuthentication));
                var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.NoResult();
        }


        private bool TryToExtractApiKeyAuthorizationHeaderValue(StringValues headers, out AuthenticationHeaderValue headerValue)
        {
            headerValue = null;

            if (!headers.Any())
            {
                return false;
            }

            headerValue = GetAuthorizationApiKeyHeaderOrDefault(headers);

            return headerValue != null;
        }

        private AuthenticationHeaderValue GetAuthorizationApiKeyHeaderOrDefault(StringValues authorizationHeaders)
        {
            return authorizationHeaders
                .Select(i =>
                {
                    if (AuthenticationHeaderValue.TryParse(i, out var headerValue))
                    {
                        return headerValue;
                    }
                    return null;
                })
                .Where(f => f != null)
                .FirstOrDefault(v => v.Scheme.Equals(ApiKeyAuthentication, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
