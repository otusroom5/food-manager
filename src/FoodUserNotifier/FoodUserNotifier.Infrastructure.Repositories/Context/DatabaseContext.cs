using FoodUserNotifier.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FoodUserNotifier.Infrastructure.Repositories.Contract;

namespace FoodUserNotifier.Infrastructure.Repositories.Context;

public class DatabaseContext : DbContext
{
    public DbSet<DeliveryReportDTO> DeliveryReports { get; set; }
    public DbSet<RecepientDTO> Recipient { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
   
  



    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecepientDTO>().HasKey(b => b.Id).HasName("recepientid");
        modelBuilder.Entity<RecepientDTO>().Property(b => b.RecepientId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<RecepientDTO>().Property(b => b.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<RecepientDTO>().Property(b => b.Telegram).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<DeliveryReportDTO>().HasKey(b => b.Id).HasName("deliveryreport");
        modelBuilder.Entity<DeliveryReportDTO>().Property(b => b.NotificationId).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<DeliveryReportDTO>().Property(b => b.Message).IsRequired().HasMaxLength(2000);
        modelBuilder.Entity<DeliveryReportDTO>().Property(b => b.Success).IsRequired().HasMaxLength(10);


        base.OnModelCreating(modelBuilder);
    }



}




