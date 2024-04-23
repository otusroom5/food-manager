namespace FoodUserNotifier.WebApi.Implementations.Options;

public class SmptClientOptions
{
    public const string SmptClient = "Smtp";
    public string HostName { get; set; }
    public string SenderAddress { get; set; }
    public string MailSubject { get; set; }
    public int Port { get; set; }
}
