using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPricesService
{
    Guid Create(PriceEntry priceEntry);
    PriceEntry Get(Guid priceEntryId);
    PriceEntry GetLast(Guid productId);
    IEnumerable<PriceEntry> GetAll(Guid productId);
}