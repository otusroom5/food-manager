using FoodSupplier.DataAccess.Abstractions;
using FoodSupplier.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodSupplier.DataAccess.Repositories;

public class ShopsRepository : IShopsRepository
{
    private readonly FoodSupplierDbContext _context;

    public ShopsRepository(FoodSupplierDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(ShopEntity shopEntity)
    {
        var result = await _context.Shops.AddAsync(shopEntity);

        return result.Entity.Id;
    }

    public async Task<ShopEntity> GetAsync(Guid shopId)
    {
        var candidate = await _context.Shops.FindAsync(shopId);

        return candidate;
    }

    public async Task<IEnumerable<ShopEntity>> GetAllAsync(bool onlyActive = false)
    {
        var entities = await _context.Shops
            .Where(x => !onlyActive || x.IsActive == true)
            .ToListAsync();

        return entities;
    }

    public void Update(ShopEntity shopEntity)
    {
        _context.Entry(shopEntity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid shopId)
    {
        var candidate = await _context.Shops.FindAsync(shopId);

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