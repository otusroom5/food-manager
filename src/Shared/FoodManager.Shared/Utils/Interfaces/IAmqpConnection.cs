namespace FoodManager.Shared.Utils.Interfaces;

public interface IAmqpConnection
{
    string GetHost();
    int GetPort();
    string GetQueueName();
    string GetUserName();
    string GetUserPassword();
}
