using FoodManager.Shared.Extensions;
using FoodManager.Shared.Auth.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithBarerAuth(); 
builder.ConfigureAuthentication();
//builder.Services.AddHttpClient("FoodStorageApi", builder.Configuration.GetConnectionString("FoodStorageApi"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.MapControllers();

app.Run();
