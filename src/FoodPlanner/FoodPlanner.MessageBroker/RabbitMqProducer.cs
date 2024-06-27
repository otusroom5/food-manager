using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FoodPlanner.MessageBroker;

public sealed class RabbitMqProduce : IRabbitMqProducer, IDisposable
{
    private readonly ILogger<RabbitMqProduce> _logger;
    private readonly IConnection _connection;
    private readonly string _queueName;

    public RabbitMqProduce(ILogger<RabbitMqProduce> logger, 
        IConfiguration configuration)
    {
        _logger = logger;

        IAmqpConnection rabbitConnection = AmqpConnectionStringBuilder.Parse(configuration.GetConnectionString("RabbitMq"));
        var factory = new ConnectionFactory
        {
            HostName = rabbitConnection.GetHost(),
            Port = rabbitConnection.GetPort(),
            UserName = rabbitConnection.GetUserName(),
            Password = rabbitConnection.GetUserPassword()
        };

        _queueName = rabbitConnection.GetQueueName();
        _connection = factory.CreateConnection();
    }
                
    public void SendReportMessage<T>(T message)
    {   
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: _queueName,
                        durable: false,
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

        _logger.LogInformation(" Report sent to quue 'notification'");
    }

    public void Dispose()
    {
        _connection.Dispose();    
    }   
}