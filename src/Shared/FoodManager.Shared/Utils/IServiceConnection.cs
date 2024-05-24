namespace FoodManager.Shared.Utils;

public interface IServiceConnection
{
    string GetSchema(string defaultSchema = "");
    string GetHost(string defaultHost = "");
    int GetPort(int defaultPort = 0);

}