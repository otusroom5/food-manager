using System.Globalization;
using FoodSupplier.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodSupplier.DataAccess;

public class FoodSupplierDbContext : DbContext
{
    public DbSet<PriceEntryEntity> PriceEntries { get; set; }
    public DbSet<ShopEntity> Shops { get; set; }

    public FoodSupplierDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture);
        base.OnConfiguring(optionsBuilder);
    }
}