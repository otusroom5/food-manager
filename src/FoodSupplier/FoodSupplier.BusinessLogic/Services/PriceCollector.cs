using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Services;

public class PriceCollector : IPriceCollector
{
    public PriceEntry Collect(Guid shopId, Guid productId)
    {
        throw new NotImplementedException();
    }
}