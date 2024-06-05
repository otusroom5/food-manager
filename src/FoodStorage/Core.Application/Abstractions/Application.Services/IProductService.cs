using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IProductService
{
    public Task<Guid> CreateAsync(ProductCreateRequestModel product);
    public Task<ProductViewModel> GetByIdAsync(Guid productId);
    public Task<ProductViewModel> GetByNameAsync(string productName);
    public Task<List<ProductViewModel>> GetAllAsync();
    public Task DeleteAsync(Guid productId);

}
