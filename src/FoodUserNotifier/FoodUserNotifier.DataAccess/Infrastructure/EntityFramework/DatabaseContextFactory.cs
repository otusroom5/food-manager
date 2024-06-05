using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace FoodUserNotifier.DataAccess.Infrastructure.EntityFramework
{
    internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Фабрика для создания контекста БД, используется для механизма миграций
        /// </summary>
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
            dbContextOptionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("FoodUserNotifier.DataAccess"));
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }

    }
}
