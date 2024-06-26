using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using FoodStorage.Application.Repositories.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FoodStorage.Infrastructure.Implementations.MessageBroker;

public sealed class RabbitMqProducer<T> : IMessageProducer<T> where T : class
{
    private readonly ILogger<RabbitMqProducer<T>> _logger;
    private readonly IConnection _connection;
    public RabbitMqProducer(ILogger<RabbitMqProducer<T>> logger, IConfiguration configuration)
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

        _connection = factory.CreateConnection();
    }
    public async Task ProduceMessageAsync(T message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: "notification",
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
}
