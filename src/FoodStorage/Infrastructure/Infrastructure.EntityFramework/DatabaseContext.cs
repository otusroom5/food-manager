using FoodStorage.Infrastructure.EntityFramework.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.EntityFramework;

public class DatabaseContext : DbContext
{
    public DbSet<ProductDto> Products { get; set; }
    public DbSet<ProductItemDto> ProductItems { get; set; }
    public DbSet<ProductHistoryDto> ProductHistoryItems { get; set; }
    public DbSet<RecipeDto> Recipes { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductItemDto>()
            .HasOne(p => p.Product)
            .WithMany(p => p.ProductItems)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(pi => pi.ProductId)
            .HasConstraintName("fk_productitem_productid")
            .IsRequired();

        modelBuilder.Entity<ProductHistoryDto>()
            .HasOne<ProductDto>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(ph => ph.ProductId)
            .HasConstraintName("fk_producthistory_productid")
            .IsRequired();

        modelBuilder.Entity<RecipeDto>().OwnsMany(
            b => b.Positions, navBuilder =>
            {
                navBuilder.WithOwner().HasForeignKey(p => p.RecipeId);

                navBuilder.HasOne<ProductDto>()
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(p => p.ProductId)
                   .HasConstraintName("fk_recipeposition_productid")
                   .IsRequired();
            });

        base.OnModelCreating(modelBuilder);
    }
}
