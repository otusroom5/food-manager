using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Services;

public interface IProductItemService
{
    public Task<ProductItemId> CreateAsync(ProductItem productItem);
    public Task<ProductItem> GetByIdAsync(ProductItemId productItemId);
    public Task<IEnumerable<ProductItem>> GetByProductIdAsync(ProductId productId);
    public Task<IEnumerable<ProductItem>> GetAllAsync();
    public Task<IEnumerable<ProductItem>> GetExpireProductItemsAsync(int daysBeforeExpired = 0);
    public Task TakePartOfAsync(ProductId productId, int count, UserId userId);
    public Task WriteOffAsync(IEnumerable<ProductItemId> productItemIds, UserId userId);
    public Task DeleteAsync(ProductItemId productItemId);
}
