using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductItemRepository
{
    public Task CreateAsync(ProductItem productItem);
    public Task<ProductItem> FindByIdAsync(ProductItemId productItemId);
    public Task<IEnumerable<ProductItem>> GetByProductIdAsync(ProductId productId);
    public Task<IEnumerable<ProductItem>> GetByIdsAsync(IEnumerable<ProductItemId> productItemIds);
    public Task<IEnumerable<ProductItem>> GetAllAsync();
    public Task ChangeAsync(ProductItem productItem);
    public Task DeleteAsync(ProductItem productItem);
}