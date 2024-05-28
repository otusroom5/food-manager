using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPriceCollector
{
    PriceEntry Collect(Guid shopId, Guid productId);
}