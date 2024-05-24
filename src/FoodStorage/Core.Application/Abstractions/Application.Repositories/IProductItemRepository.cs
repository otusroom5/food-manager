using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductItemRepository
{
    public Task CreateAsync(ProductItem productItem);
    public ProductItem FindById(ProductItemId productItemId);
    public IEnumerable<ProductItem> GetByProductId(ProductId productId);
    public IEnumerable<ProductItem> GetByIds(IEnumerable<ProductItemId> productItemIds);
    public IEnumerable<ProductItem> GetAll();
    public Task ChangeAsync(ProductItem productItem);
    public Task DeleteAsync(ProductItem productItem);
}