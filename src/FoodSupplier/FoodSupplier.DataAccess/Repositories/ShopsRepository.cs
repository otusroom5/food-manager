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

    public void Create(ShopEntity shopEntity)
    {
        _context.Shops.Add(shopEntity);
    }

    public ShopEntity Get(Guid shopId)
    {
        var candidate = _context.Shops.Find(shopId);

        return candidate;
    }

    public void Update(ShopEntity shopEntity)
    {
        _context.Entry(shopEntity).State = EntityState.Modified;
    }

    public void Delete(Guid shopId)
    {
        var candidate = _context.Shops.Find(shopId);

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