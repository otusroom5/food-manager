namespace FoodManager.Shared.Utils;

public interface IServiceConnection
{
    string GetSchema(string defaultSchema = "");
    string GetHost(string defaultHost = "");
    int GetPort(int defaultPort = 0);
    int GetVersionProtocol(int defaultVersionProtocol = 1);
}