using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductItemRepository
{
    public ProductItemId Create(ProductItem productItem);
    public ProductItem FinfById(ProductItemId productItemId);
    public IEnumerable<ProductItem> GetByProductId(ProductId productId);
    public IEnumerable<ProductItem> GetAll();
    public void Change(ProductItem productItem);
    public void Delete(ProductItem productItem);
}