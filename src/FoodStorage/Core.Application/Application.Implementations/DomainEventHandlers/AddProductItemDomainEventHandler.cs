using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public class AddProductItemDomainEventHandler : BaseDomainEventHandler<AddedProductItemDomainEvent>
{
    private readonly IProductHistoryRepository _productHistoryRepository;

    public AddProductItemDomainEventHandler(IProductHistoryRepository productHistoryRepository)
    {
        _productHistoryRepository = productHistoryRepository;
    }

    protected override async Task HandleDomainEventAsync(AddedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // запись в историю о добавлении продукта
        ProductHistory productHistoryItem = ProductHistory.CreateNew(
            id: ProductHistoryId.CreateNew(),
            productId: domainEvent.ProductId,
            state: ProductState.Added,
            count: domainEvent.ProductCount,
            createdBy: domainEvent.OccuredBy,
            createdAt: domainEvent.OccuredOn);

        _productHistoryRepository.Create(productHistoryItem);
    }
}
