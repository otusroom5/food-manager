using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class ProductItemService : IProductItemService
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductItemService> _logger;

    public ProductItemService(
        IProductItemRepository productItemRepository,
        IProductRepository productRepository,
        ILogger<ProductItemService> logger)
    {
        _productItemRepository = productItemRepository;
        _productRepository = productRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public async Task<ProductItemId> CreateAsync(ProductItem productItem)
    {
        try
        {
            // проверка существования продукта, единицу которого хотим положить в холодильник
            Product product = await _productRepository.FindByIdAsync(productItem.ProductId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productItem.ProductId.ToString());
            }

            await _productItemRepository.CreateAsync(productItem);

            return productItem.Id;
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<ProductItem> GetByIdAsync(ProductItemId productItemId)
    {
        try
        {
            ProductItem result = await _productItemRepository.FindByIdAsync(productItemId);

            if (result is null)
            {
                throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<IEnumerable<ProductItem>> GetByProductIdAsync(ProductId productId) => await _productItemRepository.GetByProductIdAsync(productId);

    public async Task<IEnumerable<ProductItem>> GetAllAsync() => await _productItemRepository.GetAllAsync();

    public async Task<IEnumerable<ProductItem>> GetExpireProductItemsAsync(int daysBeforeExpired = 0)
    {
        var result = await _productItemRepository.GetAllAsync();

        return  result.Where(r => r.ExpiryDate.AddDays(-daysBeforeExpired).Date <= DateTime.UtcNow.Date);
    }

    public async Task TakePartOfAsync(ProductId productId, int count, UserId userId)
    {
        try
        {
            Product product = await _productRepository.FindByIdAsync(productId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            // получаем все единицы продукта из холодильника, не просроченные
            var productItems = await _productItemRepository.GetByProductIdAsync(productId);
            productItems = productItems.Where(pi => pi.ExpiryDate.Date > DateTime.UtcNow.Date);

            // общее кол-во продукта в холодильнике
            int commonCount = productItems.Sum(pi => pi.Amount);

            // если общее кол-во продукта в холодильнике меньше запрашиваемого - ошибка
            if (commonCount < count)
            {
                throw new ApplicationLayerException($"The total quantity of product '{product.Name}' in the refrigerator is less than requested ({count})");
            }

            // сортировка списка по дате возрастания окончания срока годности (т.е. в первую очередь берем более старые)
            List<ProductItem> listForTakeOff = productItems.OrderBy(pi => pi.ExpiryDate).ToList();

            // забираем продукт
            // если указанное кол-во больше чем есть у продукта, то берем у него все - и переходим к следующему
            // если меньше или равно, то берем сколько есть и выходим из цикла
            foreach (var productItem in listForTakeOff)
            {
                if (count == 0) break;

                if (productItem.Amount > count)
                {
                    productItem.ReduceAmount(count, userId);
                    await _productItemRepository.ChangeAsync(productItem);
                    break;
                }
                else
                {
                    productItem.ReduceAmount(productItem.Amount, userId);
                    await _productItemRepository.DeleteAsync(productItem);
                    count -= productItem.Amount;
                }
            }
        }
        catch (Exception exception)
        {
            LogError("TakePartOf", exception);
            throw;
        }
    }

    public async Task WriteOffAsync(IEnumerable<ProductItemId> productItemIds, UserId userId)
    {
        try
        {
            // получаем все указанные единицы продукта из холодильника
            var productItems = await _productItemRepository.GetByIdsAsync(productItemIds);

            foreach (var productItem in productItems)
            {
                productItem.WriteOff(userId);
                _productItemRepository.DeleteAsync(productItem);
            }
        }
        catch (Exception exception)
        {
            LogError("WriteOff", exception);
            throw;
        }
    }

    public async Task DeleteAsync(ProductItemId productItemId)
    {
        try
        {
            ProductItem productItem = await _productItemRepository.FindByIdAsync(productItemId);

            if (productItem is null)
            {
                throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
            }

            await _productItemRepository.DeleteAsync(productItem);
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
