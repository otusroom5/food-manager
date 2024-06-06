using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using FoodUserNotifier.Core.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using FoodUserNotifier.Infrastructure.Services.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FoodUserNotifier.Infrastructure.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly IOptions<AqmpClientOptions> _options;
    private readonly ILogger _logger;
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly INotificationConverter _messageConverter;
    private bool disposedValue;
    private IConnection _connection;
    private IModel _channel;

    public NotificationService()
    {
        //IMessageDispatcher messageDispatcher,
        //INotificationConverter messageConverter,
        //IOptions<AqmpClientOptions> options, ILoggerFactory loggerFactory


        //_messageDispatcher = messageDispatcher;
        //_messageConverter = messageConverter;
        //_options = options;
        //_logger = loggerFactory.CreateLogger<NotificationService>();
    }

    public void StartListen()
    {
        var factory = new ConnectionFactory { HostName = _options.Value.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _options.Value.Queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _logger.LogInformation("Waiting for messages.");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogDebug("Received :{1}", message);

            var notification = _messageConverter.Convert(message);
            
            await _messageDispatcher.SendAllAsync(notification);
        };

        _channel.BasicConsume(queue: "hello",
                             autoAck: true,
                             consumer: consumer);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _channel.Dispose();
                _connection.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
