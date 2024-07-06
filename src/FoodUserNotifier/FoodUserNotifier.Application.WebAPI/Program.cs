using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using FoodManager.Shared.Extensions;
using Serilog;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using FoodUserNotifier.Infrastructure.Services.Implementations;
using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Infrastructure.Sender.Telegram;
using FoodUserNotifier.Infrastructure.Sender.Smtp;
using FoodUserNotifier.Infrastucture.Repositories;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.WebApi.Extensions;
using FoodUserNotifier.Application.WebAPI.Utils;
using FoodUserNotifier.Application.WebAPI.Extensions;
using FoodUserNotifier.Infrastructure.Services.Utils;
using FoodUserNotifier.Infrastructure.Sender.Smtp.Options;
using FoodUserNotifier.Infrastructure.Sender.Telegram.Options;
using FoodUserNotifier.BusinessLogic.Services;
using FoodUserNotifier.Infrastructure.Telegram.Services.Implementations;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Repositories;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("UserNotifier_");

    builder.Services.AddSerilog();

    builder.Services.Configure<SmptClientOptions>(builder.Configuration.GetSection(SmptClientOptions.SmptClient));
    builder.Services.Configure<TelegramClientOptions>(builder.Configuration.GetSection(TelegramClientOptions.TelegramClient));

    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),
            x => x.MigrationsAssembly("FoodUserNotifier.Infrastructure.Repositories"));
        
    }, ServiceLifetime.Transient);

    builder.Services.AddControllers();
    
    builder.Services.AddHttpMessageHandlers();

    builder.Services.AddHttpServiceClient(options =>
    {
        options.ServiceName = "FoodPlannerApi";
        options.ConnectionString = builder.Configuration.GetConnectionString("FoodPlannerApi");
        options.AuthenticationType = AuthenticationType.ApiKey;
        options.AuthServiceName = "UserAuthApi";
        options.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
    });
    builder.Services.AddHttpServiceClient(options =>
    {
        options.ServiceName = "UserAuthApi";
        options.ConnectionString = builder.Configuration.GetConnectionString("UserAuthApi");
        options.AuthenticationType = AuthenticationType.ApiKey;
        options.AuthServiceName = "UserAuthApi";
        options.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
    });

    builder.Services.AddHostedService<TelegramBackgroundService>();
    builder.Services.AddHostedService<NotificationBackgroundService>();
    builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();
    builder.Services.AddTransient<INotificationConverter, JsonNotificationConverter>();

    builder.Services.AddTransient<IDeliveryReportsRepository, DeliveryReportsRepository>();
    builder.Services.AddTransient<ITelegramSessionsRepository, TelegramSessionsRepository>();

    builder.Services.AddTransient<IMessageSender, TelegramMessageSender>();
    builder.Services.AddTransient<IMessageSender, SmtpMessageSender>();
    builder.Services.AddScoped<IMessageSenderCollection, MessageSenderCollection>();
    builder.Services.AddSingleton<IDomainLogger, DomainLogger>();
    builder.Services.AddRecepientsSource("UserAuthApi");
    builder.Services.AddReportsSource("FoodPlannerApi");
    builder.Services.AddSwaggerGenWithBarerAuth();

    builder.ConfigureAuthentication();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodUserNotifier API v1");
            options.RoutePrefix = string.Empty;
        });
    }
    app.UseAuthorization();

    app.UseLogMediator();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");

    app.UseEfMigration();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

return 0;


















