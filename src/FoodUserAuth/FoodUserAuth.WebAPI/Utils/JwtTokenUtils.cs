using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodUserAuth.BusinessLogic.Types;
using FoodUserAuth.WebApi.Extensions;

namespace FoodUserAuth.WebApi.Utils
{
    public static class JwtTokenUtils
    {
        /// <summary>
        /// This method generates Jwt token with username, role in claims.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns>string</returns>
        public static string GenerateToken(Options.AuthenticationOptions options, string userName, UserRole role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role.ConvertToString()),
            };

            var jwtSecToken = new JwtSecurityToken(
                    issuer: options.TokenIssuer,
                    audience: options.Audience,
                    claims: claims,
                    expires: CalculateExpiryTime(options.TokenExpirySec),
                    signingCredentials: CreateCredentials(options.SecurityKey));

            return new JwtSecurityTokenHandler().WriteToken(jwtSecToken);
        }

        private static DateTime CalculateExpiryTime(int seconds)
        {
            return DateTime.UtcNow.Add(TimeSpan.FromSeconds(seconds));
        }

        private static SigningCredentials CreateCredentials(string key)
        {
            return new SigningCredentials(SecurityKeyUtils.CreateSymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        }

    }
}
