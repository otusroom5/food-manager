using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.WebApi.Implementations.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using FoodUserNotifier.WebApi.Interfaces;

namespace FoodUserNotifier.WebApi.Services;

public class AqmpService : IAqmpService, IDisposable
{
    private readonly IOptions<AqmpClientOptions> _options;
    private readonly ILogger _logger;
    private readonly IMessageDispatcher _messageDispatcher;
    private bool disposedValue;
    private IConnection _connection;
    private IModel _channel;

    public AqmpService(IMessageDispatcher messageDispatcher, 
        IOptions<AqmpClientOptions> options, ILogger<AqmpService> logger)
    {
        _messageDispatcher = messageDispatcher;
        _options = options;
        _logger = logger;
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
            _logger.LogDebug($"Received :{message}");

            await _messageDispatcher.SendAllAsync(message);
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
