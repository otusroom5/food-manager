namespace FoodPlanner.MessageBroker;

public interface IRabbitMqProducer
{
    public void SendReportMessage<T>(T message);
}
