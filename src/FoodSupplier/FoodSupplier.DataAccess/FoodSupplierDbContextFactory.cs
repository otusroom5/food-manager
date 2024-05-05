using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodSupplier.DataAccess;

public class FoodSupplierDbContextFactory : IDesignTimeDbContextFactory<FoodSupplierDbContext>
{
    public FoodSupplierDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FoodSupplierDbContext>();
        optionsBuilder.UseNpgsql();

        return new FoodSupplierDbContext(optionsBuilder.Options);
    }
}