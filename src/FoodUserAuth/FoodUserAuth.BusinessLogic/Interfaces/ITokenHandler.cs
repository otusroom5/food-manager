using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.WebApi.Utils;

public interface ITokenHandler
{
    /// <summary>
    /// Extract Api Key If from token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ApiKeyData ExtractApiKeyData(string token);

    /// <summary>
    /// This method generates Jwt token with Id of api key into database.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="userName"></param>
    /// <param name="role"></param>
    /// <returns>string</returns>
    string GenerateApiToken(Guid apiKeyId, Guid userId);

    /// <summary>
    /// This method generates Jwt token with username, role in claims.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="userName"></param>
    /// <param name="role"></param>
    /// <returns>string</returns>
    string Generate(string loginName, Guid id, UserRole role);
}