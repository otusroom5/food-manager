using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using FoodStorage.Application.Repositories.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FoodStorage.Infrastructure.Implementations.MessageBroker;

public sealed class MessageProducer : IMessageProducer, IDisposable
{
    private readonly ILogger<MessageProducer> _logger;
    private readonly IConnection _connection;
    private readonly string _queueName;

    public MessageProducer(ILogger<MessageProducer> logger, IConfiguration configuration)
    {
        _logger = logger;

        IAmqpConnection brokerConnection = AmqpConnectionStringBuilder.Parse(configuration["BrokerConnection"]);
        var factory = new ConnectionFactory
        {
            HostName = brokerConnection.GetHost(),
            Port = brokerConnection.GetPort(),
            UserName = brokerConnection.GetUserName(),
            Password = brokerConnection.GetUserPassword()
        };

        _queueName = brokerConnection.GetQueueName();
        _connection = factory.CreateConnection();
    }

    public void Send<T>(T message)
    {
        _logger.LogInformation("Getting {0} for sending to message broker", typeof(T).Name);

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
                             routingKey: _queueName,
                             basicProperties: properties,
                             body: body);

        _logger.LogInformation("{0} sent to queue '{1}'", typeof(T).Name, _queueName);
    }

    public void Dispose()
    {
        _connection.Close();
    }
}
