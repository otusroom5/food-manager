using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Repositories.MessageBroker;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.Common.DomainEvents;
using FoodStorage.Domain.Entities.Common.Events;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.UnitEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.DomainEventHandlers;

/// <summary>
/// Обработчик доменного события взятия продукта
/// </summary>
public class ReduceProductItemDomainEventHandler : BaseDomainEventHandler<ReducedProductItemDomainEvent>
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IMessageProducer _messageProducer;

    public ReduceProductItemDomainEventHandler(
        IProductItemRepository productItemRepository,
        IProductHistoryRepository productHistoryRepository,
        IProductRepository productRepository,
        IUnitRepository unitRepository,
        IMessageProducer messageProducer,
        ILogger<ReduceProductItemDomainEventHandler> logger) : base(logger)
    {
        _productItemRepository = productItemRepository;
        _productHistoryRepository = productHistoryRepository;
        _productRepository = productRepository;
        _unitRepository = unitRepository;
        _messageProducer = messageProducer;
    }

    protected override async Task HandleDomainEventAsync(ReducedProductItemDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productId = ProductId.FromGuid(domainEvent.ProductId);

        // запись в историю о взятии продукта
        ProductHistory productHistoryItem = ProductHistory.CreateNew(
            id: ProductHistoryId.CreateNew(),
            productId: productId,
            state: ProductActionType.Taken,
            count: domainEvent.ProductCount,
            createdBy: UserId.FromGuid(domainEvent.OccuredBy),
            createdAt: domainEvent.OccuredOn);

        await _productHistoryRepository.CreateAsync(productHistoryItem);

        // Проверка на остаток продукта в холодильнике. Если продукта осталось меньше, чем мин остаток на день, то отправка сообщения в брокер
        Product product = await _productRepository.FindByIdAsync(productId);
        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        var productItems = await _productItemRepository.GetByProductIdAsync(productId);
        double countProductInBase = productItems.Sum(pi => pi.Amount);
        if (countProductInBase <= product.MinAmountPerDay)
        {
            // Определяем основную единицу измерения для этого продукта, чтобы отправить сообщение с этой инфой
            var units = await _unitRepository.GetByTypeAsync(product.UnitType);
            Unit unit = units.FirstOrDefault(u => u.IsMain);

            // Формирование сообщения и отправка его
            ProductEndingEventMessage productEndingEvent = 
                new(product.Name.ToString(), product.MinAmountPerDay, countProductInBase, unit?.Id.ToString(), domainEvent.OccuredOn);
            
            _messageProducer.Send(productEndingEvent);
        }
    }
}
