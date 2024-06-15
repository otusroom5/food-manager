using System.Net.Http.Headers;
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
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Repositories;
using FoodUserNotifier.Application.WebAPI.Interfaces;
using FoodUserNotifier.Application.WebAPI.Controllers;

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
    });




    builder.Services.AddControllers();

    builder.Services.AddHttpMessageHandlers();

    

    builder.Services.AddHttpServiceClient(options =>
    {
        options.ServiceName = "UserAuthApi";
        options.ConnectionString = builder.Configuration.GetConnectionString("UserAuthApi");
        options.AuthenticationType = AuthenticationType.ApiKey;
        options.AuthServiceName = "UserAuthApi";
        options.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
    });


    builder.Services.AddHostedService<NotificationBackgroundService>();
    builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();
    builder.Services.AddTransient<INotificationConverter, JsonNotificationConverter>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
 //   builder.Services.AddTransient<IMessageSender, TelegramMessageSender>();
    builder.Services.AddTransient<IMessageSender, SmtpMessageSender>();
    builder.Services.AddTransient<IMessageSenderCollection, MessageSenderCollection>();
    builder.Services.AddSingleton<IDomainLogger, DomainLogger>();
    builder.Services.AddSingleton<IRecepientRepository, RecepientRepository>();
    builder.Services.AddSingleton<IDeliveryReportsRepository, DeliveryReportsRepository>();
    builder.Services.AddSingleton<IRecepientController, RecepientController>();


    builder.Services.AddRecepientsSource("UserAuthApi");
    builder.Services.AddSwaggerGenWithBarerAuth();

    builder.Services.AddWebEncoders();
    builder.Services.AddSwaggerGen();

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

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();



    app.UseStaticFiles();

    app.UseRouting();

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


















