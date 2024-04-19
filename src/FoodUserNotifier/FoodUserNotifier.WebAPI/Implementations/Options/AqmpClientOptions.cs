namespace FoodUserNotifier.WebApi.Implementations.Options;

public class AqmpClientOptions
{
    public const string AqmpClient = "Aqmp";
    public string HostName { get; set; }
    public string Queue { get; set; }
}
