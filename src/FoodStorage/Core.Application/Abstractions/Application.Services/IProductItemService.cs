using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IProductItemService
{
    public Task<Guid> CreateAsync(ProductItemCreateRequestModel productItem, Guid userId);
    public Task<ProductItemViewModel> GetByIdAsync(Guid productItemId);
    public Task<List<ProductItemViewModel>> GetByProductIdAsync(Guid productId);
    public Task<List<ProductItemViewModel>> GetAllAsync();
    public Task<List<ProductItemViewModel>> GetExpireProductItemsAsync(int daysBeforeExpired = 0);
    public Task<List<ProductItemViewModel>> GetEndingProductItemsAsync();
    public Task TakePartOfAsync(Guid productId, int count, Guid userId);
    public Task WriteOffAsync(IEnumerable<Guid> productItemIds, Guid userId);
    public Task DeleteAsync(Guid productItemId);
}
