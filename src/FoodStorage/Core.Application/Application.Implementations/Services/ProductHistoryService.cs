using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Implementations.Common.Extensions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class ProductHistoryService : IProductHistoryService
{
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly ILogger<ProductHistoryService> _logger;

    public ProductHistoryService(
        IProductHistoryRepository productHistoryRepository,
        IProductRepository productRepository,
        IUnitRepository unitRepository,
        ILogger<ProductHistoryService> logger)
    {
        _productHistoryRepository = productHistoryRepository;
        _productRepository = productRepository;
        _unitRepository = unitRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public async Task<List<ProductHistoryViewModel>> GetActionsWithProductByDateAsync(Guid productId, DateTime date)
    {
        try
        {
            ProductId productIdEntity = ProductId.FromGuid(productId);
            Product product = await _productRepository.FindByIdAsync(productIdEntity);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            var units = await _unitRepository.GetByTypeAsync(product.UnitType);
            var unit = units.FirstOrDefault(u => u.IsMain);

            var productHistories = await _productHistoryRepository.GetByProductIdAsync(productIdEntity);


            return productHistories.Where(ph => ph.CreatedAt.Date == date.Date)
                                   .Select(ph => ph.ToViewModel(product, unit))
                                   .ToList();
        }
        catch (Exception exception)
        {
            LogError("GetActionsWithProductByDate", exception);
            throw;
        }
    }

    public async Task<List<ProductHistoryViewModel>> GetActionsWithProductByUserInDateAsync(Guid userId, DateTime date)
    {
        try
        {
            List<ProductHistoryViewModel> result = new();

            var productHistories = await _productHistoryRepository.GetByUserIdAsync(UserId.FromGuid(userId));
            productHistories = productHistories.Where(ph => ph.CreatedAt.Date == date.Date);

            var products = await _productRepository.GetByIdsAsync(productHistories.Select(pi => pi.ProductId).Distinct());
            var units = await _unitRepository.GetAllAsync();

            foreach(var productHistory in productHistories)
            {
                var product = products.FirstOrDefault(p => p.Id == productHistory.ProductId);
                var unit = units.FirstOrDefault(u => u.UnitType == product.UnitType && u.IsMain);

                result.Add(productHistory.ToViewModel(product, unit));
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetActionsWithProductByUserInDate", exception);
            throw;
        }
    }

    public async Task<List<ProductHistoryViewModel>> GetProductsByStateInDateIntervalAsync(string state, DateTime dateStart, DateTime dateEnd)
    {
        try
        {
            List<ProductHistoryViewModel> result = new();

            if (!Enum.TryParse<ProductState>(state, true, out var productState))
            {
                throw new InvalidEnumValueException(nameof(state), state, nameof(ProductState));
            }

            var productHistories = await _productHistoryRepository.GetByStateAsync(productState);
            productHistories = productHistories.Where(ph => ph.CreatedAt.Date >= dateStart && ph.CreatedAt.Date <= dateEnd);

            var products = await _productRepository.GetByIdsAsync(productHistories.Select(pi => pi.ProductId).Distinct());
            var units = await _unitRepository.GetAllAsync();

            foreach (var productHistory in productHistories)
            {
                var product = products.FirstOrDefault(p => p.Id == productHistory.ProductId);
                var unit = units.FirstOrDefault(u => u.UnitType == product.UnitType && u.IsMain);

                result.Add(productHistory.ToViewModel(product, unit));
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetProductsByStateInDateInterval", exception);
            throw;
        }
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
