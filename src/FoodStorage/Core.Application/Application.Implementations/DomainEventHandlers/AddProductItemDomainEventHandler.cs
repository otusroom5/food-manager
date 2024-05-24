using FoodStorage.Domain.Entities.Common.DomainEvents;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public class AddProductItemDomainEventHandler : BaseDomainEventHandler<AddedProductItemDomainEvent>
{
    protected override async Task HandleDomainEventAsync(AddedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
