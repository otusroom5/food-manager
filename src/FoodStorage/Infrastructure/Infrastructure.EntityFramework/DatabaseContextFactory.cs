using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FoodStorage.Infrastructure.EntityFramework;

/// <summary>
/// Фабрика для создания контекста БД, используется для механизма миграций
/// </summary>
public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        // Получение директории с appsettings
        string currentDirectory = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
        string path = Directory.GetParent(currentDirectory)?.FullName + @"\FoodStorage.WebApi";

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();

        var connectionString = configuration["DbConnection"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InfrastructureException("Connection string is empty");
        }
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("FoodStorage.Infrastructure.EntityFramework"));
        return new DatabaseContext(dbContextOptionsBuilder.Options);
    }
}
