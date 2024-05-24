using FoodStorage.Domain.Entities.Common.DomainEvents;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public class ReduceProductItemDomainEventHandler : BaseDomainEventHandler<ReducedProductItemDomainEvent>
{
    protected override async Task HandleDomainEventAsync(ReducedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
