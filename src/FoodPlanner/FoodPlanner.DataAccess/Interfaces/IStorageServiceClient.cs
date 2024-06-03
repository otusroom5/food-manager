using FoodPlanner.DataAccess.Models;

namespace FoodPlanner.DataAccess.Interfaces;

public interface IStorageServiceClient
{
    Task<List<ProductDto>> GetExpiredProductsAsync();    
}
