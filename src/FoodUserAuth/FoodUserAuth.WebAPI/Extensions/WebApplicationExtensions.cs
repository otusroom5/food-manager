using FoodUserAuth.DataAccess.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FoodUserAuth.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void UseEfMigration(this WebApplication application)
    {
        using (var databaseContextScope = application.Services.CreateScope())
        {
            var database = databaseContextScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            database.Database.Migrate();
        }
        Log.Logger.Information("Database is migrated!!!");
    }
}
