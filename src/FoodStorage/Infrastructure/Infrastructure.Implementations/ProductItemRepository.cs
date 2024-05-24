using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.Implementations;

internal class ProductItemRepository : IProductItemRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMediator _mediator;

    public ProductItemRepository(DatabaseContext databaseContext, IMediator mediator)
    {
        _mediator = mediator;
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task CreateAsync(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Add(productItemDto);

        await SaveChangesAsync(productItem);
    }

    public ProductItem FindById(ProductItemId productItemId)
    {
        ProductItemDto productItemDto = _databaseContext.ProductItems.Include(pi => pi.Product).FirstOrDefault(pi => pi.Id == productItemId.ToGuid());
        return productItemDto is null ? null : productItemDto.ToEntity();
    }

    public IEnumerable<ProductItem> GetByProductId(ProductId productId)
    {
        IEnumerable<ProductItemDto> productItemDtos = _databaseContext.ProductItems.Include(pi => pi.Product).Where(pi => pi.ProductId == productId.ToGuid());
        return productItemDtos.Select(pi => pi.ToEntity()).ToList();
    }

    public IEnumerable<ProductItem> GetByIds(IEnumerable<ProductItemId> productItemIds)
    {
        var guidIds = productItemIds.Select(pi => pi.ToGuid());

        IEnumerable<ProductItemDto> productItemDtos = _databaseContext.ProductItems.Include(pi => pi.Product).Where(pi => guidIds.Contains(pi.Id));
        return productItemDtos.Select(pi => pi.ToEntity()).ToList();
    }

    public IEnumerable<ProductItem> GetAll() => _databaseContext.ProductItems.Include(pi => pi.Product).Select(pi => pi.ToEntity()).ToList();

    public async Task ChangeAsync(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Update(productItemDto);

        await SaveChangesAsync(productItem);
    }


    public async Task DeleteAsync(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Remove(productItemDto);

        await SaveChangesAsync(productItem);
    }

    private async Task SaveChangesAsync(ProductItem productItem)
    {
        // публикация(вызов обработчиков) всех зарегистрированных событий
        IEnumerable<Task> publishTasks = productItem.DomainEvents.Select(e => _mediator.Publish(e));
        await Task.WhenAll(publishTasks); 

        _databaseContext.SaveChanges();
    }
}
