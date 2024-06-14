namespace FoodPlanner.EventBusRabbitMQ;

public interface IRabbitMQProducer
{
    public void SendReportMessage<T>(T message);
}
