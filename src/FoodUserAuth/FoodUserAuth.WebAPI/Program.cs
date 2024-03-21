using FoodUserAuth.WebApi.Options;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.BusinessLogic.Services;
using FoodUserAuth.BusinessLogic.Abstractions;
using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.DataAccess.Repositories;
using static FoodUserAuth.BusinessLogic.Services.UserVerificationService;
using FoodUserAuth.BusinessLogic.Utils;
using FoodUserAuth.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection<AuthenticationOptions>());

builder.Services.AddDbContext<UserDbContext>(options =>
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
builder.Services.AddJwtAuthentication(builder.Configuration.GetOptionsOrCreateDefaults<AuthenticationOptions>());
builder.Services.AddScoped<IUserVerificationService, UserVerificationService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordHasher, MD5PasswordHasher>();

builder.Services.AddAuthorization();
builder.Services.AddMapper();

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
