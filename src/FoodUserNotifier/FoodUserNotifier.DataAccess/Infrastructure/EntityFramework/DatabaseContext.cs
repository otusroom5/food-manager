using FoodUserNotifier.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.DataAccess.Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RecepientDTO> Recipient { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecepientDTO>().HasOne<RecepientDTO>();
            modelBuilder.Entity<RecepientDTO>().HasKey(b => b.Id);
            modelBuilder.Entity<RecepientDTO>().Property(b=>b.RoleEnum).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<RecepientDTO>().Property(b => b.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<RecepientDTO>().Property(b => b.Telegram).IsRequired().HasMaxLength(100);
            

            base.OnModelCreating(modelBuilder);

        }
    }
}
