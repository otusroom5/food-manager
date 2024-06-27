namespace FoodStorage.Application.Repositories.MessageBroker;

public interface IMessageProducer
{
    void Send<T>(T message);
}