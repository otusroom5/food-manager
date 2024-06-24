using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.UnitEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.Implementations;

internal class UnitRepository : IUnitRepository
{
    private readonly DatabaseContext _databaseContext;

    public UnitRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task CreateAsync(Unit unit)
    {
        if (unit is null)
        {
            throw new EmptyArgumentValueException(nameof(unit));
        }

        UnitDto dbUnit = await _databaseContext.Units.FirstOrDefaultAsync(u => u.UnitType == unit.UnitType.ToString() && u.Coefficient == 1);

        if (unit.IsMain && dbUnit is not null)
        {
            throw new InfrastructureException($"Cannot add one more main unit in {unit.UnitType} group");
        }

        UnitDto unitDto = unit.ToDto();
        _databaseContext.Units.Add(unitDto);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<Unit> FindByIdAsync(UnitId unitId)
    {
        UnitDto unitDto = await _databaseContext.Units.FindAsync(unitId.ToString());
        return unitDto?.ToEntity();
    }

    public async Task<IEnumerable<Unit>> GetByTypeAsync(UnitTypeE unitType)
    {
        var units = _databaseContext.Units.Where(u => u.UnitType.ToLower() == unitType.ToString().ToLower());
        var result = await units.Select(u => u.ToEntity()).ToListAsync();
        
        return result;
    }

    public async Task<IEnumerable<Unit>> GetAllAsync() => await _databaseContext.Units.Select(u => u.ToEntity()).ToListAsync();

    public async Task DeleteAsync(Unit unit)
    {
        if (unit is null)
        {
            throw new EmptyArgumentValueException(nameof(unit));
        }

        if (unit.IsMain)
        {
            throw new InfrastructureException($"Cannot delete the main unit in {unit.UnitType} group");
        }

        UnitDto unitDto = unit.ToDto();
        _databaseContext.Units.Remove(unitDto);

        await _databaseContext.SaveChangesAsync();
    }
}
