namespace FoodUserNotifier.Infrastructure.Services.Options;

public class AqmpClientOptions
{
    public const string AqmpClient = "Aqmp";
    public string HostName { get; set; }
    public string Queue { get; set; }
}
