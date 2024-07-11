using FoodManager.Shared.Utils.Implementations;
using FoodManager.Shared.Utils.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FoodPlanner.BusinessLogic.Interfaces;


namespace FoodPlanner.BusinessLogic.MessageBroker;

public class RabbitMqConsumer : BackgroundService
{
    private const string QueueName = "storage";

    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private IModel _channel;

    public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger,
        IServiceProvider serviceProvider,
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
        consumer.Received += async (model, ea) =>
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var reportDistributionService = serviceScope.ServiceProvider.GetRequiredService<IReportDistributionService>();
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogDebug("Received {Message}", message);
                await reportDistributionService.DistributeAsync(message);
            }
        };

        _channel.BasicConsume(queue: QueueName,
                     autoAck: true,
                     consumer: consumer);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

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
