using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.Implementations;

internal class ProductItemRepository : IProductItemRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductItemRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Create(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Add(productItemDto);
        _databaseContext.SaveChanges();
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

    public void Change(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Update(productItemDto);

        _databaseContext.SaveChanges();
    }


    public void Delete(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Remove(productItemDto);

        _databaseContext.SaveChanges();
    }
}
