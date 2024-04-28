using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using 


namespace FoodUserNotifier.DataAcess.Infrastructure.EntityFramework
{
    /// <summary>
    /// Фабрика для создания контекста БД, используется для механизма миграций
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // Получение директории с appsettings
            string currentDirectory = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
            string path = Directory.GetParent(currentDirectory)?.FullName + @"\FoodUserNotifier.WebApi";

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionString = configuration["DbConnection"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("Connection string is empty"); // Заменить на локальный Exception при его реализации
            }
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("FoodUserNotifier.DataAcess.Infrastructure.EntityFramework"));
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }

    }
}
