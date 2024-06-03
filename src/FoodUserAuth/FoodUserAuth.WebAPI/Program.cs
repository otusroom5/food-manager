using FoodUserAuth.BusinessLogic.Services;
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
using System.Text.Json.Serialization;
using System;
using Serilog;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.BusinessLogic.Implementations;
using FoodUserAuth.WebApi.Utils;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("UserAuth_");

    builder.Services.AddSerilog();

    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
            x => x.MigrationsAssembly("FoodUserAuth.DataAccess"));
    });

    builder.Services.AddControllers()
                    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateJsonConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerGenWithBarerAuth();
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
    builder.Services.AddScoped<ITokenHandler, JwtTokenHandler>();
    builder.Services.AddScoped<IPasswordGenerator, DefaultPasswordGenerator>();
    builder.Services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
    
    builder.ConfigureAuthentication();

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

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");

    app.UseEfMigration();

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