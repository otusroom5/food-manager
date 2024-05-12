using FoodSupplier.BusinessLogic.Dto;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IShopsService
{
    Guid Create(ShopDto shop);
    ShopDto Get(Guid shopId);
    void Update(ShopDto shop);
    void Delete(Guid shopId);
}