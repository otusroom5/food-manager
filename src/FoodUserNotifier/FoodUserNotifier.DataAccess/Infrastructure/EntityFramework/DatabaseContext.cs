using FoodUserNotifier.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FoodUserNotifier.DataAccess.Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<RecepientDTO> Recipient { get; set; }
        public string DbPath { get; }

        DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {    
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecepientDTO>().HasOne<RecepientDTO>();
            modelBuilder.Entity<RecepientDTO>().HasKey(b => b.Id).HasName("recepientid");
            modelBuilder.Entity<RecepientDTO>().Property(b=>b.RoleEnum).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<RecepientDTO>().Property(b => b.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<RecepientDTO>().Property(b => b.Telegram).IsRequired().HasMaxLength(100);
            
            base.OnModelCreating(modelBuilder);
        }
       

    }
}
