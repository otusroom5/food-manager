using UserAuth.WebApi.Options;
using UserAuth.WebApi.Extensions;
using UserAuth.BusinessLogic.Services;
using UserAuth.BusinessLogic.Abstractions;
using UserAuth.DataAccess.Abstractions;
using UserAuth.DataAccess.Repositories;
using static UserAuth.BusinessLogic.Services.UserVerificationService;
using UserAuth.BusinessLogic.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection<AuthenticationOptions>());

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
