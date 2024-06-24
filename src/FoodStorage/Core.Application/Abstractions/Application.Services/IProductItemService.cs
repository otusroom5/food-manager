using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IProductItemService
{
    public Task<Guid> CreateAsync(ProductItemCreateRequestModel productItem, Guid userId);
    public Task<ProductItemViewModel> GetByIdAsync(Guid productItemId, string unit);
    public Task<List<ProductItemViewModel>> GetByProductIdAsync(Guid productId, string unit);
    public Task<List<ProductItemViewModel>> GetAllAsync();
    public Task<List<ProductItemViewModel>> GetExpireProductItemsAsync(int daysBeforeExpired = 0);
    public Task<List<ProductItemViewModel>> GetEndingProductItemsAsync();
    public Task TakePartOfAsync(ProductItemTakePartOfRequestModel request, Guid userId);
    public Task WriteOffAsync(IEnumerable<Guid> productItemIds, Guid userId);
    public Task DeleteAsync(Guid productItemId);
}
