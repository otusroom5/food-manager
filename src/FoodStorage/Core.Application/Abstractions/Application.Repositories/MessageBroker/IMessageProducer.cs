namespace FoodStorage.Application.Repositories.MessageBroker;

public interface IMessageProducer<in T> where T : class
{
    Task ProduceMessageAsync(T message);
}