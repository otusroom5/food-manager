using FoodManager.Shared.Extensions;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Reports;
using FoodPlanner.BusinessLogic.Services;
using FoodPlanner.DataAccess.Implementations;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("FoodPlanner_");

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithBarerAuth();

builder.Services.AddHttpMessageHandlers();
builder.Services.AddHttpServiceClient("UserAuthApi", builder.Configuration.GetConnectionString("UserAuthApi"));
builder.Services.AddHttpServiceClient(options =>
{
    options.ServiceName = "FoodStorageApi";
    options.ConnectionString = builder.Configuration.GetConnectionString("FoodStorageApi");
    options.AuthenticationType = AuthenticationType.ApiKey;
    options.AuthServiceName = "UserAuthApi";
    options.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
});

builder.Services.AddHttpClient<IStorageRepository, StorageRepository>("FoodStorageApi");
builder.Services.AddHttpClient<IUnitOfWork, UnitOfWork>("FoodStorageApi");
builder.Services.AddScoped<IReportService, ReportService>();

builder.ConfigureAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodPlanner API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
