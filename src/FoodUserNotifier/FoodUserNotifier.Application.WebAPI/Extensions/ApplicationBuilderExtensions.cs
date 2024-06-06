using Serilog;

using FoodUserNotifier.Application.WebAPI.Utils;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Infrastructure.Services.Interfaces;

namespace FoodUserNotifier.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    static LogMediator _logMediator;

    public static void UseNotificationService(this WebApplication app)
    {
        

        //INotificationService notificationService = app.Services.GetService<INotificationService>();
        //notificationService.StartListen();
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
