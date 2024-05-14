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
        var database = application.Services.GetRequiredService<DatabaseContext>();
        database.Database.Migrate();
        Log.Logger.Information("Database is migrated!!!");
    }
}
