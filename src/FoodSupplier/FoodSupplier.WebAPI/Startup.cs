using FoodSupplier.DataAccess;
using FoodSupplier.WebAPI.MapperProfiles;
using FoodSupplier.WebAPI.Modules;
using Microsoft.EntityFrameworkCore;

namespace FoodSupplier.WebAPI;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddLogging(log =>
        {
            log.AddSimpleConsole(opt =>
            {
                opt.SingleLine = true;
                opt.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
            });
        });
        services.AddSwaggerGen();

        ConfigureDatabase(services, Configuration);

        services.AddAutoMapper(typeof(SupplierMappingProfile));

        services.AddRepositories();
        services.AddServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supplier Api V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        ConfigureMiddleware(app);
    }

    private static void ConfigureMiddleware(IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FoodSupplierDbContext>();
        dbContext.Database.Migrate();
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("DbConnection").Value;

        services.AddDbContext<FoodSupplierDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                .ConfigureWarnings(b => b.Default(WarningBehavior.Throw));
        }, ServiceLifetime.Transient);
    }
}