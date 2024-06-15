namespace FoodManager.Shared.Utils.Interfaces;

public interface IAmqpConnectionString
{
    string GetHost();
    int GetPort();
    string GetQueueName();
    string GetUserName();
    string GetUserPassword();
}
