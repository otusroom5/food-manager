using FoodStorage.Domain.Entities.Common.DomainEvents;
using MediatR;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public abstract class BaseDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> 
    where TDomainEvent : BaseDomainEvent
{
    public async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            await HandleDomainEventAsync(domainEvent, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message); //-----------------------------------
        }
    }

    protected abstract Task HandleDomainEventAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
