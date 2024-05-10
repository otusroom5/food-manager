using FoodUserAuth.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; }
}
