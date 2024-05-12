using FoodSupplier.BusinessLogic.Dto;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPriceCollectorService
{
    PriceEntryDto Collect(Guid shopId, Guid productId);
}