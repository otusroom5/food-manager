namespace FoodPlanner.EventBusRabbitMQ;

public interface IRabbitMqProducer
{
    public void SendReportMessage<T>(T message);
}
