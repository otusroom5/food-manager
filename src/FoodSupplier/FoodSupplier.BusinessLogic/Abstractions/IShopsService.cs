using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IShopsService
{
    Task<Guid> CreateAsync(Shop shop);
    Task<Shop> GetAsync(Guid shopId);
    Task<IEnumerable<Shop>> GetAllAsync(bool onlyActive = false);
    void Update(Shop shop);
    void Delete(Guid shopId);
}