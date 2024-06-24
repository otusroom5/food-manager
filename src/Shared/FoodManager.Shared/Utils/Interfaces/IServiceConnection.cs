namespace FoodManager.Shared.Utils.Interfaces;

public interface IServiceConnection
{
    string GetSchema(string defaultSchema = "");
    string GetHost(string defaultHost = "");
    int GetPort(int defaultPort = 0);

}