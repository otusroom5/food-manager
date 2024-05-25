using FoodManager.WebUI.Extensions;
using FoodManager.WebUI.Options;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("ManagerUI_");

    builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.Authentication));
    builder.Services.AddSerilog();
    builder.Services.AddControllersWithViews();
    builder.Services.AddCookieAuthentication(options =>
    {
        options.LoadFromConfiguration(builder.Configuration);
    }, "Account/SignIn");

    builder.Services.AddHttpClient("UserAuthApi", builder.Configuration.GetConnectionString("UserAuthApi"));
    builder.Services.AddHttpClient("FoodStorageApi", builder.Configuration.GetConnectionString("FoodStorageApi"));
    builder.Services.AddHttpClient("FoodSupplierApi", builder.Configuration.GetConnectionString("FoodSupplierApi"));
    builder.Services.AddHttpClient("FoodPlannerApi", builder.Configuration.GetConnectionString("FoodPlannerApi"));

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Account/Error");
    }

    app.UseRouting();
    app.UseStaticFiles();

    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=SignIn}");
    app.MapAreaControllerRoute(
        name: "administrator_area",
        areaName: "Administrator",
        pattern: "{area:exists}/{controller=Administrator}/{action=Index}/{id?}");
    app.MapAreaControllerRoute(
        name: "cooker_area",
        areaName: "Cooker",
        pattern: "{area:exists}/{controller=Cooker}/{action=Index}/{id?}");
    app.MapAreaControllerRoute(
        name: "manager_area",
        areaName: "Manager",
        pattern: "{area:exists}/{controller=Manager}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

return 0;
