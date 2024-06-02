using FoodManager.Shared.Auth.Extensions;
using FoodManager.Shared.Extensions;
using FoodStorage.Application.Implementations;
using FoodStorage.Infrastructure.Implementations;
using FoodStorage.WebApi.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddSwaggerGenWithBarerAuth();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage Api V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.UseEfMigration();

app.Run();
