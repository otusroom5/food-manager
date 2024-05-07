using FoodUserAuth.WebApi.Options;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.BusinessLogic.Services;
using FoodUserAuth.BusinessLogic.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Implementations;

var builder = WebApplication.CreateBuilder(args);

var authenticationOptions = new AuthenticationOptions();
builder.Configuration.GetSection(AuthenticationOptions.Authentication).Bind(authenticationOptions);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("UserAuthConnection"));
});

builder.Services.AddControllers();
builder.Services.AddLogging(options=>
    {
        options.SetMinimumLevel(LogLevel.Debug);
        options.AddConsole();
    });

builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(authenticationOptions);
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordGenerator, DefaultPasswordGenerator>();
builder.Services.AddScoped<IPasswordHasher, MD5PasswordHasher>();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();

app.Run();
