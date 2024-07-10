using FoodPlanner.DataAccess.Entities;

namespace FoodPlanner.DataAccess.Interfaces;

public interface ISupplierRepository
{    
    Task<PriceEntity> GetActualProductPriceAsync(Guid productId);
}
