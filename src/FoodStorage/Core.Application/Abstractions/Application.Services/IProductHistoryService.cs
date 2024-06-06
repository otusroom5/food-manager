using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IProductHistoryService
{
    public Task<List<ProductHistoryViewModel>> GetProductsByStateInDateIntervalAsync(string state, DateTime dateStart, DateTime dateEnd);
    public Task<List<ProductHistoryViewModel>> GetActionsWithProductByDateAsync(Guid productId, DateTime date);
    public Task<List<ProductHistoryViewModel>> GetActionsWithProductByUserInDateAsync(Guid userId, DateTime date);
}
