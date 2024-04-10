using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Implementations;

public class ProductItemService : IProductItemService
{
    public ProductItemId Create(ProductItem productItem)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProductItemId productItemId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public ProductItem GetById(ProductItemId productItemId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductItem> GetByProductId(ProductId productId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductItem> GetExpiredProductItems()
    {
        throw new NotImplementedException();
    }

    public void TakePartOf(ProductId productId, int count)
    {
        throw new NotImplementedException();
    }

    public void WriteOff(IEnumerable<ProductItemId> productItemIds)
    {
        throw new NotImplementedException();
    }
}
