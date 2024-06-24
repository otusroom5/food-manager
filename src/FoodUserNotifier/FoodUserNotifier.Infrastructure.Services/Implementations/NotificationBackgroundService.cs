using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using FoodUserNotifier.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FoodManager.Shared.Utils.Interfaces;
using FoodManager.Shared.Utils.Implementations;

namespace FoodUserNotifier.Infrastructure.Services.Implementations;

public class NotificationBackgroundService : BackgroundService, INotificationBackgroundService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IModel _channel;

    public NotificationBackgroundService(IServiceProvider serviceProvider, 
        ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger<NotificationBackgroundService>();
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }

    public void StartListen()
    {
        IAmqpConnection rabbitConnection = AmqpConnectionStringBuilder.Parse(_configuration.GetConnectionString("RabbitMq"));

        var factory = new ConnectionFactory
        {
            HostName = rabbitConnection.GetHost(),
            Port = rabbitConnection.GetPort(),
            UserName = rabbitConnection.GetUserName(),
            Password = rabbitConnection.GetUserPassword()
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: rabbitConnection.GetQueueName(),
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
                var messageDispatcher = serviceScope.ServiceProvider.GetRequiredService<IMessageDispatcher>();
                var notificationConverter = serviceScope.ServiceProvider.GetRequiredService<INotificationConverter>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogDebug("Received :{1}", message);

                var notification = notificationConverter.Convert(message);
                await messageDispatcher.SendAllAsync(notification);
            }
        };

        _channel.BasicConsume(queue: rabbitConnection.GetQueueName(),
                     autoAck: true,
                     consumer: consumer);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartListen();

        return Task.CompletedTask;
    }
}
