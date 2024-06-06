using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.Application.WebAPI.Extensions;

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