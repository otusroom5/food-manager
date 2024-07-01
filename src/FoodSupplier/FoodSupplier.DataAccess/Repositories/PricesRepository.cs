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

    public async Task<Guid> CreateAsync(PriceEntryEntity priceEntryEntity)
    {
        var result = await _context.PriceEntries.AddAsync(priceEntryEntity);

        return result.Entity.Id;
    }

    public async Task<PriceEntryEntity> GetAsync(Guid priceEntryId)
    {
        var candidate = await _context.PriceEntries.FindAsync(priceEntryId);

        return candidate;
    }

    public async Task<PriceEntryEntity> GetLastAsync(Guid productId)
    {
        var candidate = await _context.PriceEntries
            .Where(p => p.ProductId == productId)
            .OrderByDescending(p => p.Date)
            .FirstOrDefaultAsync();

        return candidate;
    }

    public async Task<IEnumerable<PriceEntryEntity>> GetAllAsync(Guid productId)
    {
        return await _context.PriceEntries.Where(x => x.ProductId == productId).ToListAsync();
    }

    public void Update(PriceEntryEntity priceEntryEntity)
    {
        _context.Entry(priceEntryEntity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid priceEntryId)
    {
        var candidate = await _context.PriceEntries.FindAsync(priceEntryId);

        if (candidate is not null)
        {
            _context.Remove(candidate);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}