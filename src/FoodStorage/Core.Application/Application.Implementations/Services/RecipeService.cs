using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Implementations.Common.Extensions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Domain.Entities.UnitEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(
        IRecipeRepository recipeRepository, 
        IProductRepository productRepository, 
        IUnitRepository unitRepository, 
        ILogger<RecipeService> logger)
    {
        _recipeRepository = recipeRepository;
        _productRepository = productRepository;
        _unitRepository = unitRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public async Task<Guid> CreateAsync(RecipeCreateRequestModel recipe)
    {
        try
        {
            var recipeEntity = recipe.ToEntity();

            // проверка на существование рецепта с таким же именем
            var recipeWithSameName = await _recipeRepository.FindByNameAsync(recipeEntity.Name);
            if (recipeWithSameName is not null)
            {
                throw new ApplicationLayerException($"{nameof(Recipe)} with same name '{recipe.Name}' is already exists");
            }

            // проверка существования продуктов, кот. будут в рецепте
            foreach (var recipePosition in recipeEntity.Positions)
            {
                Product product = await _productRepository.FindByIdAsync(recipePosition.ProductId);
                if (product is null)
                {
                    throw new EntityNotFoundException(nameof(Product), recipePosition.ProductId.ToString());
                }

                // Проверка указанной для продукта единицы измерения
                await GetUnit(product.UnitType, recipePosition.UnitId.ToString());
            }

            await _recipeRepository.CreateAsync(recipeEntity);

            return recipeEntity.Id.ToGuid();
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<RecipeViewModel> GetByIdAsync(Guid recipeId)
    {
        try
        {
            var recipeEntityId = RecipeId.FromGuid(recipeId);

            Recipe recipe = await _recipeRepository.FindByIdAsync(recipeEntityId);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeEntityId.ToString());
            }

            var products = await _productRepository.GetByIdsAsync(recipe.Positions.Select(p => p.ProductId));

            var recipePositionsVm = recipe.Positions.Select(p => p.ToViewModel(products.FirstOrDefault(pr => pr.Id == p.ProductId))).ToList();
            RecipeViewModel result = recipe.ToViewModel(recipePositionsVm);

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<RecipeViewModel> GetByNameAsync(string recipeName)
    {
        try
        {
            var recipeEntityName = RecipeName.FromString(recipeName);

            Recipe recipe = await _recipeRepository.FindByNameAsync(recipeEntityName);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeEntityName.ToString());
            }

            var products = await _productRepository.GetByIdsAsync(recipe.Positions.Select(p => p.ProductId));

            var recipePositionsVm = recipe.Positions.Select(p => p.ToViewModel(products.FirstOrDefault(pr => pr.Id == p.ProductId))).ToList();
            RecipeViewModel result = recipe.ToViewModel(recipePositionsVm);

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetByName", exception);
            throw;
        }
    }

    public async Task<List<RecipeViewModel>> GetByProductIdAsync(Guid productId)
    {
        try
        {
            var productEntityId = ProductId.FromGuid(productId);

            var recipes = await _recipeRepository.GetByProductIdAsync(productEntityId);

            Product product = await _productRepository.FindByIdAsync(productEntityId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productEntityId.ToString());
            }

            return recipes.Select(r => r.ToViewModel(r.Positions.Select(p => p.ToViewModel(product)).ToList())).ToList();
        }
        catch (Exception exception)
        {
            LogError("GetByProductId", exception);
            throw;
        }
    }

    public async Task<List<RecipeViewModel>> GetAllAsync()
    {
        try
        {
            var recipes = await _recipeRepository.GetAllAsync();

            // Сбор идентификаторов продуктов и взятие их из бд
            List<ProductId> productIds = new();
            foreach (var recipe in recipes)
            {
                productIds.AddRange(recipe.Positions.Select(p => p.ProductId).Distinct());
            }
            var products = await _productRepository.GetByIdsAsync(productIds);

            // Формирование результата
            List<RecipeViewModel> result = new();
            foreach (var recipe in recipes)
            {
                var recipePositionsVm = recipe.Positions.Select(p => p.ToViewModel(products.FirstOrDefault(pr => pr.Id == p.ProductId))).ToList();
                result.Add(recipe.ToViewModel(recipePositionsVm));
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetByProductId", exception);
            throw;
        }
    }

    public async Task UpdateAsync(RecipeUpdateRequestModel recipe)
    {
        try
        {
            Recipe recipeEntity = recipe.ToEntity();

            // проверка на существование этого рецепта в базе
            var recipeFromBase = await _recipeRepository.FindByIdAsync(recipeEntity.Id);
            if (recipeFromBase is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipe.Id.ToString());
            }

            // проверка существования продуктов, кот. будут в рецепте
            foreach (var recipePosition in recipeEntity.Positions)
            {
                Product product = await _productRepository.FindByIdAsync(recipePosition.ProductId);
                if (product is null)
                {
                    throw new EntityNotFoundException(nameof(Product), recipePosition.ProductId.ToString());
                }

                // Проверка указанной для продукта единицы измерения
                await GetUnit(product.UnitType, recipePosition.UnitId.ToString());
            }

            await _recipeRepository.ChangeAsync(recipeEntity);
        }
        catch (Exception exception)
        {
            LogError("Update", exception);
            throw;
        }
    }

    public async Task DeleteAsync(Guid recipeId)
    {
        try
        {
            var recipeEntityId = RecipeId.FromGuid(recipeId);

            // проверка на существование этого рецепта в базе
            var recipe = await _recipeRepository.FindByIdAsync(recipeEntityId);

            if (recipe is null)
            {
                throw new EntityNotFoundException(nameof(Recipe), recipeEntityId.ToString());
            }

            await _recipeRepository.DeleteAsync(recipe);
        }
        catch (Exception exception)
        {
            LogError("Delete", exception);
            throw;
        }
    }
    
    private async Task<Unit> GetUnit(UnitType unitType, string unit)
    {
        // проверка существования указанной единицы измерения в типе
        var units = await _unitRepository.GetByTypeAsync(unitType);
        Unit unitFromBase = units.FirstOrDefault(u => u.Id == UnitId.FromString(unit));
        if (unitFromBase is null)
        {
            throw new EntityNotFoundException(nameof(Unit), unit);
        }

        return unitFromBase;
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
