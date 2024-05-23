using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IShopsRepository
{
    Guid Create(ShopEntity shopEntity);
    ShopEntity Get(Guid shopId);
    void Update(ShopEntity shopEntity);
    void Delete(Guid shopId);
    void Save();
}