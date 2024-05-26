﻿using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

/// <summary>
/// Обработчик доменного события взятия продукта
/// </summary>
public class ReduceProductItemDomainEventHandler : BaseDomainEventHandler<ReducedProductItemDomainEvent>
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly IProductRepository _productRepository;

    public ReduceProductItemDomainEventHandler(
        IProductItemRepository productItemRepository,
        IProductHistoryRepository productHistoryRepository,
        IProductRepository productRepository)
    {
        _productItemRepository = productItemRepository;
        _productHistoryRepository = productHistoryRepository;
        _productRepository = productRepository;
    }

    protected override async Task HandleDomainEventAsync(ReducedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        ProductId productId = domainEvent.ProductItem.ProductId;

        // запись в историю о взятии продукта
        ProductHistory productHistoryItem = ProductHistory.CreateNew(
            id: ProductHistoryId.CreateNew(),
            productId: productId,
            state: ProductState.Taken,
            count: domainEvent.ProductCount,
            createdBy: domainEvent.OccuredBy,
            createdAt: domainEvent.OccuredOn);

        _productHistoryRepository.Create(productHistoryItem);

        // Проверка на остаток продукта в холодильнике. Если продукта осталось меньше, чем мин остаток на день, то отправка сообщения в брокер
        Product product = _productRepository.FindById(productId);
        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        var productItems = _productItemRepository.GetByProductId(productId);
        int countProductInBase = productItems.Sum(pi => pi.Amount);
        if (countProductInBase <= product.MinAmountPerDay)
        {
            throw new NotImplementedException("Will be rabbit message"); //TODO
        }
    }
}
