using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Interfaces;

public interface IApiKeyRepository
{
    void Create(ApiKey token);
    void Update(ApiKey user);
    void Delete(ApiKey token);
    Task<IEnumerable<ApiKey>> GetAllAsync();
    Task<ApiKey> GetByIdOrDefaultAsync(Guid id);
}
