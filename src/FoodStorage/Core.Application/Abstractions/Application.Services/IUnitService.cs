using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;

namespace FoodStorage.Application.Services;

public interface IUnitService
{
    public Task<string> CreateAsync(UnitCreateRequestModel unit);
    public Task<UnitViewModel> GetByIdAsync(string unitId);
    public Task<List<UnitViewModel>> GetByTypeAsync(string unitType);
    public Task<List<UnitViewModel>> GetAllAsync();
    public Task<List<string>> GetAllTypesAsync();
    public Task<UnitViewModel> GetMainByTypeAsync(string unitType);
    public Task DeleteAsync(string unitId);
}
