using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public abstract class BaseDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : BaseDomainEvent
{
    private readonly ILogger _logger;

    protected BaseDomainEventHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("'{0}' request '{1}' handling.", GetType().Name, typeof(TDomainEvent).Name);
            await HandleDomainEventAsync(notification, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogCritical("'{0}' request '{1}' exception. \n{2}", GetType().Name, typeof(TDomainEvent).Name, exception);
            throw new ApplicationLayerException($"Error in handler of domain event '{notification}': {exception.Message}");
        }
        finally
        {
            _logger.LogInformation("'{0}' request '{1}' handled.", GetType().Name, typeof(TDomainEvent).Name);
        }
    }

    protected abstract Task HandleDomainEventAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
