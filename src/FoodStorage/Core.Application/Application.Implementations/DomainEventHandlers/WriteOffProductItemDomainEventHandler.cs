using FoodStorage.Domain.Entities.Common.DomainEvents;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public class WriteOffProductItemDomainEventHandler : BaseDomainEventHandler<WritedOffProductItemDomainEvent>
{
    protected override async Task HandleDomainEventAsync(WritedOffProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
