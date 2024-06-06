namespace FoodUserNotifier.Infrastructure.Sender.Smtp.Options;

public class SmptClientOptions
{
    public const string SmptClient = "Smtp";
    public string HostName { get; set; }
    public int Port { get; set; }
}
