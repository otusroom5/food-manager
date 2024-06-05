using FoodUserNotifier.BusinessLogic.Abstractions;
using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.BusinessLogic.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FoodUserNotifier.BusinessLogic.Contracts;

namespace FoodUserNotifier.BusinessLogic.Services;

public class MessageDispatcher : IMessageDispatcher
{
    private readonly ILogger _logger;
    private readonly IMessageConverter _messageConverter;
    private readonly IServiceProvider _serviceProvider;
    private readonly IRecepientRepository _recepientRepository;

    public MessageDispatcher(IMessageConverter messageConverter, 
        IRecepientRepository recepientRepository,
        IServiceProvider serviceProvider, ILogger logger)
    {
        _logger = logger;
        _messageConverter = messageConverter;
        _recepientRepository = recepientRepository;
        _serviceProvider = serviceProvider;
    }

    public async Task SendAllAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            _logger?.LogWarning("Неверное сообщение от AmqpClient");
            return;
        }

        try
        {
            DataMessage dataMessage = _messageConverter.Convert(message);

            IEnumerable<RecepientDTO> recepients = _recepientRepository.GetAllForRole(dataMessage.Role);

            foreach (IMessageSender service in _serviceProvider.GetServices<IMessageSender>())
            {
                await   service.SendAsync(recepients, dataMessage.Data);
            }
        }
        catch (FormatException ex)
        {
            _logger?.LogError(ex, "Не возможно десериализовать сообщение");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }
    }
}
