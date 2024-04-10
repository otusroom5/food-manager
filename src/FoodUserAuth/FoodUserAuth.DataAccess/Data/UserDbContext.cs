using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Types;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<User>()
               .Property(e => e.State)
               .HasConversion(
                   v => v.ToString(),
                   v => (UserState)Enum.Parse(typeof(UserState), v));
        }
    }
}
