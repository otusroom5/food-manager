using FoodPlanner.DataAccess.Models;

namespace FoodPlanner.DataAccess.Interfaces;

public interface IStorageRepository
{    
    Task<List<ProductDto>> GetExpiredProductsAsync();
}
