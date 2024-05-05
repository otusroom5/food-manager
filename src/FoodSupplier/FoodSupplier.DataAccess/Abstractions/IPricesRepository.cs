using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.DataAccess.Abstractions;

public interface IPricesRepository
{
    void Create(PriceEntryEntity priceEntryEntity);
    PriceEntryEntity Get(Guid priceEntryId);
    PriceEntryEntity GetLast(Guid productId);
    PriceEntryEntity[] GetAll();
    void Update(PriceEntryEntity priceEntryEntity);
    void Delete(Guid priceEntryId);
    void Save();
}