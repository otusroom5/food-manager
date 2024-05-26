using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using MediatR;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public abstract class BaseDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> 
    where TDomainEvent : BaseDomainEvent
{
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await HandleDomainEventAsync(notification, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApplicationLayerException($"Error in handler of domain event '{notification}': {ex.Message}");
        }
    }

    protected abstract Task HandleDomainEventAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
