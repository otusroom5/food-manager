using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IApiKeyService
{
    Task<ApiKeyDto> CreateApiKeyAsync(DateTime expiryDate);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ApiKeyDto>> GetKeysAsync();
    Task<string> RenewApiKeyAsync(string token);
}
