using FoodSupplier.BusinessLogic.Dto;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPricesService
{
    Guid Create(PriceEntryDto priceEntry);
    PriceEntryDto Get(Guid priceEntryId);
    PriceEntryDto GetLast(Guid productId);
}