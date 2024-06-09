using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace FoodUserNotifier.DataAccess.Infrastructure.EntityFramework
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Фабрика для создания контекста БД, используется для механизма миграций
        /// </summary>
      public DatabaseContext CreateDbContext(string[] args)
      {
          
          var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
          dbContextOptionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FoodStorage;Username=postgres;Password=postgre");
          return new DatabaseContext(dbContextOptionsBuilder.Options);
      }

    }
}
