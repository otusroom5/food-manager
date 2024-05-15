using FoodUserNotifier.DataAcess.Infrastructure.EntityFramework.Contracts;
using Microsoft.EntityFrameworkCore;


namespace FoodUserNotifier.DataAcess.Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecepientDTO>()
                .HasOne<RecepientDTO>(); // прописать связь с таблицей UserAuthDb
        }
    }
}
