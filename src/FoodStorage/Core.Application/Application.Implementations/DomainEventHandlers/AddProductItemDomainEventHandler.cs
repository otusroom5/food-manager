using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

public class AddProductItemDomainEventHandler : BaseDomainEventHandler<AddedProductItemDomainEvent>
{
    private readonly IProductHistoryRepository _productHistoryRepository;

    public AddProductItemDomainEventHandler(IProductHistoryRepository productHistoryRepository, ILogger<AddProductItemDomainEventHandler> logger) 
        : base(logger)
    {
        _productHistoryRepository = productHistoryRepository;
    }

    protected override async Task HandleDomainEventAsync(AddedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // запись в историю о добавлении продукта
        ProductHistory productHistoryItem = ProductHistory.CreateNew(
            id: ProductHistoryId.CreateNew(),
            productId: ProductId.FromGuid(domainEvent.ProductId),
            state: ProductActionType.Added,
            count: domainEvent.ProductCount,
            createdBy: UserId.FromGuid(domainEvent.OccuredBy),
            createdAt: domainEvent.OccuredOn);

        await _productHistoryRepository.CreateAsync(productHistoryItem);
    }
}
