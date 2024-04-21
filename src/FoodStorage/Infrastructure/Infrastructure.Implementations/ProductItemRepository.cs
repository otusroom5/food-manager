using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

namespace FoodStorage.Infrastructure.Implementations;

internal class ProductItemRepository : IProductItemRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductItemRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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
        ProductItemDto productItemDto = _databaseContext.ProductItems.FirstOrDefault(pi => pi.Id == productItemId.ToGuid());
        return productItemDto is null ? null : productItemDto.ToEntity();
    }

    public IEnumerable<ProductItem> GetByProductId(ProductId productId)
    {
        IEnumerable<ProductItemDto> productItemDtos = _databaseContext.ProductItems.Where(pi => pi.ProductId == productId.ToGuid());
        return productItemDtos.Select(pi => pi.ToEntity()).ToList();
    }

    public IEnumerable<ProductItem> GetAll()
    {
        return _databaseContext.ProductItems.Select(pi => pi.ToEntity()).ToList();
    }

    public void Change(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Update(productItemDto);
    }


    public void Delete(ProductItem productItem)
    {
        if (productItem is null)
        {
            throw new EmptyArgumentValueException(nameof(productItem));
        }

        ProductItemDto productItemDto = productItem.ToDto();
        _databaseContext.ProductItems.Remove(productItemDto);
    }
}
