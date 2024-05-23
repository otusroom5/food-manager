using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IPricesRepository
{
    Guid Create(PriceEntryEntity priceEntryEntity);
    PriceEntryEntity Get(Guid priceEntryId);
    PriceEntryEntity GetLast(Guid productId);
    IEnumerable<PriceEntryEntity> GetAll(Guid productId);
    void Update(PriceEntryEntity priceEntryEntity);
    void Delete(Guid priceEntryId);
    void Save();
}