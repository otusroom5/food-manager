using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IShopsRepository
{
    void Create(ShopEntity shopEntity);
    ShopEntity Get(Guid shopId);
    void Update(ShopEntity shopEntity);
    void Delete(Guid shopId);
    void Save();
}