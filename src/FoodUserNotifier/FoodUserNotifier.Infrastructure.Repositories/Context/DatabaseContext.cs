using FoodUserNotifier.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.Infrastructure.Repositories.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DeliveryReport> DeliveryReports { get; set; }
}
