using FoodUserAuth.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FoodUserAuth.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApiKey>()
            .Property(f => f.ExpiryDate)
            .HasConversion(
            v => v.ToString("yyyy-MM-dd"),
            v => DateTime.ParseExact(v, "yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}
