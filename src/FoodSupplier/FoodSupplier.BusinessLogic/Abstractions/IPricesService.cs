using FoodSupplier.BusinessLogic.Models;

namespace FoodSupplier.BusinessLogic.Abstractions;

public interface IPricesService
{
    Task<Guid> CreateAsync(PriceEntry priceEntry);
    Task<PriceEntry> GetAsync(Guid priceEntryId);
    Task<PriceEntry> GetLastAsync(Guid productId);
    Task<IEnumerable<PriceEntry>> GetAllAsync(Guid productId);
}