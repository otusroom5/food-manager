namespace FoodPlanner.BusinessLogic.MessageBroker;

public interface IRabbitMqProducer
{
    public void SendReportMessage<T>(T message);
}
