using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IShopsService
{
    Guid Create(Shop shop);
    Shop Get(Guid shopId);
    void Update(Shop shop);
    void Delete(Guid shopId);
}