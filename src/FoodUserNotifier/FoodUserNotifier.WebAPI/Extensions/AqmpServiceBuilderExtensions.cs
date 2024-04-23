using FoodUserNotifier.WebApi.Interfaces;

namespace FoodUserNotifier.WebApi.Extensions;

public static class AqmpServiceBuilderExtensions
{
    public static void UseAqmpService(this IApplicationBuilder app)
    {
        IAqmpService aqmpService = app.ApplicationServices.GetService<IAqmpService>();
        aqmpService.StartListen();
    }
}
