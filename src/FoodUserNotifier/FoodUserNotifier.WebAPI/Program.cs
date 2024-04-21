using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Implementations;
using FoodUserNotifier.DataAccess.Interfaces;
using FoodUserNotifier.WebApi.Extensions;
using FoodUserNotifier.WebApi.Implementations;
using FoodUserNotifier.WebApi.Implementations.Options;
using FoodUserNotifier.WebApi.Interfaces;
using FoodUserNotifier.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AqmpClientOptions>(builder.Configuration.GetSection(AqmpClientOptions.AqmpClient));
builder.Services.Configure<TelegramClientOptions>(builder.Configuration.GetSection(TelegramClientOptions.TelegramClient));
builder.Services.Configure<SmptClientOptions>(builder.Configuration.GetSection(AqmpClientOptions.AqmpClient));

builder.Services.AddTransient<IMessageSender, TelegramMessageSender>();
builder.Services.AddTransient<IMessageSender, SmtpMessageSender>();
builder.Services.AddTransient<IRecepientRepository, RecepientRepository>();
builder.Services.AddSingleton<IAqmpService, AqmpService>();

var app = builder.Build();

app.UseAqmpService();

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

app.Run();


