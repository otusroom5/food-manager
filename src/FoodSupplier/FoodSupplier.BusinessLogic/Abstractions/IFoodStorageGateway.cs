using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IFoodStorageGateway
{
    Task<List<Product>> GetAllProductsAsync();
}