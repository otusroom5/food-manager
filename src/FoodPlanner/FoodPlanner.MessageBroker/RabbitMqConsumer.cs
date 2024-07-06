using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace FoodPlanner.MessageBroker;

public class RabbitMqConsumer: BackgroundService
{
    private const string QueueName = "storage";

    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private IModel _channel;

    public RabbitMqConsumer(IServiceProvider serviceProvider, 
                            ILogger<RabbitMqConsumer> logger,
                            IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

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

    public void StartListen()
    { 
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _logger.LogInformation("Waiting for messages.");      

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation($"Received {message}");
        };        
      
        _channel.BasicConsume(queue: QueueName,
                     autoAck: true,
                     consumer: consumer);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartListen();

        return Task.CompletedTask;
    }
    
    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
