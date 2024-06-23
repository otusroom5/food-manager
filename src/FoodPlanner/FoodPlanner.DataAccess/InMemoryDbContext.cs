using FoodPlanner.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.DataAccess;

public class InMemoryDbContext : DbContext
{
    public InMemoryDbContext(DbContextOptions options): base(options) { }

    public DbSet<ReportEntity> ReportsInMemory { get; set; }
}
