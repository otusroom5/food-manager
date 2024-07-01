using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IShopsRepository
{
    Task<Guid> CreateAsync(ShopEntity shopEntity);
    Task<ShopEntity> GetAsync(Guid shopId);
    Task<IEnumerable<ShopEntity>> GetAllAsync(bool onlyActive = false);
    void Update(ShopEntity shopEntity);
    Task DeleteAsync(Guid shopId);
    Task SaveAsync();
}