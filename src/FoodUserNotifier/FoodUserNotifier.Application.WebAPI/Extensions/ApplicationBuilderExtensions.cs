using Serilog;

using FoodUserNotifier.Application.WebAPI.Utils;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    static LogMediator _logMediator;

    public static void UseEfMigration(this WebApplication application)
    {
        using (var databaseContextScope = application.Services.CreateScope())
        {
            var database = databaseContextScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            database.Database.Migrate();
        }
    }

    public static void UseLogMediator(this IApplicationBuilder app)
    {
        if (_logMediator != null)
        {
            throw new InvalidOperationException();
        }

        IDomainLogger domailLogger = app.ApplicationServices.GetService<IDomainLogger>();
        _logMediator = new LogMediator(Log.Logger, domailLogger);
    }
}
