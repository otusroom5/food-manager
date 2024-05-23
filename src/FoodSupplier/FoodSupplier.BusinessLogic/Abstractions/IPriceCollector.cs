using FoodSupplier.BusinessLogic.Dto;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPriceCollector
{
    PriceEntryDto Collect(Guid shopId, Guid productId);
}