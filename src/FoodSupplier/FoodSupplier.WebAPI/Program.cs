using FoodSupplier.WebAPI;
using FoodManager.Shared.Extensions;
using FoodSupplier.DataAccess;
using FoodSupplier.WebAPI.MapperProfiles;
using FoodSupplier.WebAPI.Modules;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("FoodSupplier_");

builder.Services.AddControllers();
builder.Services.AddLogging(log =>
{
    log.AddSimpleConsole(opt =>
    {
        opt.SingleLine = true;
        opt.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    });
});

ConfigureDatabase(builder.Services, builder.Configuration);

builder.Services.AddAutoMapper(typeof(SupplierMappingProfile));

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddFoodStorageClient(builder.Configuration);

builder.ConfigureAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supplier Api V1");
    c.RoutePrefix = string.Empty;
});

// app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

ConfigureMiddleware(app);


app.Run();

static void ConfigureMiddleware(IApplicationBuilder builder)
{
    using var scope = builder.ApplicationServices.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FoodSupplierDbContext>();
    dbContext.Database.Migrate();
}

static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DbConnection");

    services.AddDbContext<FoodSupplierDbContext>(options =>
    {
        options.UseNpgsql(connectionString)
            .ConfigureWarnings(b => b.Default(WarningBehavior.Throw));
    }, ServiceLifetime.Transient);
}