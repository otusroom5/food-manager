using FoodStorage.Application.Implementations;
using FoodStorage.Infrastructure.Implementations;

namespace FoodStorage.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddLogging();
        services.AddSwaggerGen();

        services.AddApplication();
        services.AddInfrastructure(Configuration);
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
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage Api V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
