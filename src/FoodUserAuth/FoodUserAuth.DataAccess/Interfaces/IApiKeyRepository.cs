using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Interfaces;

public interface IApiKeyRepository
{
    void Create(ApiKey key);
    void Update(ApiKey key);
    void Delete(ApiKey key);
    Task<IEnumerable<ApiKey>> GetAllAsync();
    Task<ApiKey> GetByIdOrDefaultAsync(Guid id);
}
