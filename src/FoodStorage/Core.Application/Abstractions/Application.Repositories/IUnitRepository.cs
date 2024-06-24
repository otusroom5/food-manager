using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Application.Repositories;

public interface IUnitRepository
{
    public Task CreateAsync(Unit unit);
    public Task<Unit> FindByIdAsync(UnitId unitId);
    public Task<IEnumerable<Unit>> GetByTypeAsync(UnitTypeE unitType);
    public Task<IEnumerable<Unit>> GetAllAsync();
    public Task DeleteAsync(Unit unit);
}
