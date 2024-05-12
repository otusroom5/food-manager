using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Dto;

namespace FoodSupplier.BusinessLogic.Services;

public class PriceCollector : IPriceCollector
{
    public PriceEntryDto Collect(Guid shopId, Guid productId)
    {
        throw new NotImplementedException();
    }
}