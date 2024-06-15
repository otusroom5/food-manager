using FoodStorage.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.WebApi.Common;

public static class WebApplicationExtensions
{
    public static void UseEfMigration(this WebApplication application)
    {
        using (var databaseContextScope = application.Services.CreateScope())
        {
            var database = databaseContextScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            database.Database.Migrate();
        }
    }
}
