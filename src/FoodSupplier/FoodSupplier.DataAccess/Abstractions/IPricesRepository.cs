using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IPricesRepository
{
    Task<Guid> CreateAsync(PriceEntryEntity priceEntryEntity);
    Task<PriceEntryEntity> GetAsync(Guid priceEntryId);
    Task<PriceEntryEntity> GetLastAsync(Guid productId);
    Task<IEnumerable<PriceEntryEntity>> GetAllAsync(Guid productId);
    void Update(PriceEntryEntity priceEntryEntity);
    Task DeleteAsync(Guid priceEntryId);
    Task SaveAsync();
}