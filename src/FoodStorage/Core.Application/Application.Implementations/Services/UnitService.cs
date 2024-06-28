using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Implementations.Common.Extensions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.UnitEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly ILogger<UnitService> _logger;

    public UnitService(IUnitRepository unitRepository, ILogger<UnitService> logger)
    {
        _unitRepository = unitRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public async Task<string> CreateAsync(UnitCreateRequestModel unit)
    {
        try
        {
            Unit unitEntity = unit.ToEntity();

            // Получаем все единицы измерения в базе
            var units = await _unitRepository.GetAllAsync();

            // проверка на существование единицы измерения с таким же наименованием
            if (units.Any(u => u.Name == unitEntity.Name))
            {
                throw new ApplicationLayerException($"{nameof(Unit)} with same name '{unitEntity.Name}' is already exists");
            }

            // проверка на существование стандарта в этой группе, если текущая единица заявлена как стандарт
            if (unitEntity.IsMain && units.Any(u => u.UnitType == unitEntity.UnitType && u.IsMain))
            {
                throw new ApplicationLayerException($"The main unit in unittype {unitEntity.UnitType} is already exists");
            }

            await _unitRepository.CreateAsync(unitEntity);

            return unitEntity.Id.ToString();
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<UnitViewModel> GetByIdAsync(string unitId)
    {
        try
        {
            var unitEntityId = UnitId.FromString(unitId);

            Unit unit = await _unitRepository.FindByIdAsync(unitEntityId);

            if (unit is null)
            {
                throw new EntityNotFoundException(nameof(Unit), unitEntityId.ToString());
            }

            return unit.ToViewModel();
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<List<UnitViewModel>> GetByTypeAsync(string unitType)
    {
        if (!Enum.TryParse<UnitType>(unitType, true, out var unitTypeValue))
        {
            throw new InvalidEnumValueException(nameof(unitType), unitType, nameof(UnitType));
        }

        var result = await _unitRepository.GetByTypeAsync(unitTypeValue);

        return result.Select(r => r.ToViewModel()).ToList();
    }


    public async Task<List<UnitViewModel>> GetAllAsync()
    {
        try
        {
            var units = await _unitRepository.GetAllAsync();

            return units.Select(u => u.ToViewModel()).ToList();
        }
        catch (Exception exception)
        {
            LogError("GetAll", exception);
            throw;
        }
    }

    public Task<List<string>> GetAllTypesAsync()
    {
        List<string> result = new();

        foreach (var value in Enum.GetValues(typeof(UnitType)))
        {
            result.Add(value.ToString());
        }

        return Task.FromResult(result);
    }

    public async Task<UnitViewModel> GetMainByTypeAsync(string unitType)
    {
        if (!Enum.TryParse<UnitType>(unitType, true, out var unitTypeValue))
        {
            throw new InvalidEnumValueException(nameof(unitType), unitType, nameof(UnitType));
        }

        var units = await _unitRepository.GetByTypeAsync(unitTypeValue);
        Unit mainUnit = units.FirstOrDefault(u => u.IsMain);

        if (mainUnit is null)
        {
            throw new EntityNotFoundException(nameof(Unit), unitType);
        }

        return mainUnit.ToViewModel();
    }

    public async Task DeleteAsync(string unitId)
    {
        try
        {
            var unitEntityId = UnitId.FromString(unitId);

            Unit unit = await _unitRepository.FindByIdAsync(unitEntityId);
            if (unit is null)
            {
                throw new EntityNotFoundException(nameof(Unit), unitEntityId.ToString());
            }

            if (unit.IsMain)
            {
                throw new ApplicationLayerException($"Can not delete the main unit in {unit.UnitType} group");
            }

            await _unitRepository.DeleteAsync(unit);
        }
        catch (Exception exception)
        {
            LogError("Delete", exception);
            throw;
        }
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
