using FoodSupplier.DataAccess.Abstractions;
using FoodSupplier.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodSupplier.DataAccess.Repositories;

public class PricesRepository : IPricesRepository
{
    private readonly FoodSupplierDbContext _context;

    public PricesRepository(FoodSupplierDbContext context)
    {
        _context = context;
    }

    public Guid Create(PriceEntryEntity priceEntryEntity)
    {
        var result = _context.PriceEntries.Add(priceEntryEntity);

        return result.Entity.Id;
    }

    public PriceEntryEntity Get(Guid priceEntryId)
    {
        var candidate = _context.PriceEntries.Find(priceEntryId);

        return candidate;
    }

    public PriceEntryEntity GetLast(Guid productId)
    {
        var candidate = _context.PriceEntries
            .Where(p => p.ProductId == productId)
            .OrderByDescending(p => p.Date)
            .FirstOrDefault();

        return candidate;
    }

    public IEnumerable<PriceEntryEntity> GetAll(Guid productId)
    {
        return _context.PriceEntries.Where(x => x.ProductId == productId).ToList();
    }

    public void Update(PriceEntryEntity priceEntryEntity)
    {
        _context.Entry(priceEntryEntity).State = EntityState.Modified;
    }

    public void Delete(Guid priceEntryId)
    {
        var candidate = _context.PriceEntries.Find(priceEntryId);

        if (candidate is not null)
        {
            _context.Remove(candidate);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}