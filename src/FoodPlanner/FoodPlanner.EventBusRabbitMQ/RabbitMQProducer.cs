using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FoodPlanner.EventBusRabbitMQ;

public class RabbitMQProduce : IRabbitMQProducer, IDisposable
{
    private readonly ILogger<RabbitMQProduce> _logger;
    private readonly IConnection _connection;
    public RabbitMQProduce(ILogger<RabbitMQProduce> logger)
    {
        _logger = logger;

        // ToDo: Move to config
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "room5",
            Password = "room5Password"
        };
        _connection = factory.CreateConnection();
    }
                
    public void SendReportMessage<T>(T message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: "notification",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: "",
                                routingKey: "notification",
                                basicProperties: properties,
                                body: body);         

        _logger.LogInformation($" Report sent to quue 'notification'");
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}