using FoodUserAuth.BusinessLogic.Services;
using FoodUserAuth.BusinessLogic.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Implementations;
using FoodManager.Shared.Auth.Extensions;
using FoodManager.Shared.Extensions;
using FoodManager.Shared.Auth.Options;
using System.Text.Json.Serialization;
using System;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("UserAuth_");

    builder.Services.Configure<JwtAuthenticationOptions>(builder.Configuration.GetSection(JwtAuthenticationOptions.Authentication));

    builder.Services.AddSerilog();

    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    });

    builder.Services.AddControllers()
                    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

    builder.Services.AddSwaggerGenWithBarerAuth();
    builder.Services.AddJwtAuthentication(options =>
    {
        options.LoadFromConfiguration(builder.Configuration);
    });
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IPasswordGenerator, DefaultPasswordGenerator>();
    builder.Services.AddScoped<IPasswordHasher, MD5PasswordHasher>();
    builder.Services.AddAuthorization();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodUserAuth API v1");
            options.RoutePrefix = string.Empty;
        });
    }
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
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