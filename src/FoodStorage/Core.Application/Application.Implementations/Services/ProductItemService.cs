using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Implementations.Common.Extensions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using Microsoft.Extensions.Logging;
using System.Linq;

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

    public async Task<Guid> CreateAsync(ProductItemCreateRequestModel productItem, Guid userId)
    {
        ProductItem productItemEntity = productItem.ToEntity(UserId.FromGuid(userId));

        try
        {
            // проверка существования продукта, единицу которого хотим положить в холодильник
            Product product = await _productRepository.FindByIdAsync(productItemEntity.ProductId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productItemEntity.ProductId.ToString());
            }

            await _productItemRepository.CreateAsync(productItemEntity);

            return productItemEntity.Id.ToGuid();
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<ProductItemViewModel> GetByIdAsync(Guid productItemId)
    {
        try
        {
            ProductItem productItem = await _productItemRepository.FindByIdAsync(ProductItemId.FromGuid(productItemId));

            if (productItem is null)
            {
                throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
            }

            Product product = await _productRepository.FindByIdAsync(productItem.ProductId);

            return productItem.ToViewModel(product);
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<List<ProductItemViewModel>> GetByProductIdAsync(Guid productId)
    {
        try
        {
            ProductId productIdEntity = ProductId.FromGuid(productId);

            Product product = await _productRepository.FindByIdAsync(productIdEntity);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            var productItems = await _productItemRepository.GetByProductIdAsync(productIdEntity);

            return productItems.Select(pi => pi.ToViewModel(product)).ToList();
        }
        catch (Exception exception)
        {
            LogError("GetByProductId", exception);
            throw;
        }
    }

    public async Task<List<ProductItemViewModel>> GetAllAsync() 
    {
        try
        {
            var productItems = await _productItemRepository.GetAllAsync();
            var products = await _productRepository.GetByIdsAsync(productItems.Select(pi => pi.ProductId).Distinct());

            return productItems.Select(pi => pi.ToViewModel(products.FirstOrDefault(p => p.Id == pi.ProductId))).ToList();
        }
        catch (Exception exception)
        {
            LogError("GetAll", exception);
            throw;
        }
    }

    public async Task<List<ProductItemViewModel>> GetExpireProductItemsAsync(int daysBeforeExpired = 0)
    {
        try
        {
            var productItems = await _productItemRepository.GetAllAsync();
            productItems = productItems.Where(r => r.ExpiryDate.AddDays(-daysBeforeExpired).Date <= DateTime.UtcNow.Date);

            var products = await _productRepository.GetByIdsAsync(productItems.Select(pi => pi.ProductId).Distinct());

            return productItems.Select(pi => pi.ToViewModel(products.FirstOrDefault(p => p.Id == pi.ProductId)))
                               .ToList();
        }
        catch (Exception exception)
        {
            LogError("GetExpireProductItems", exception);
            throw;
        }
    }

    public async Task<List<ProductItemViewModel>> GetEndingProductItemsAsync()
    {
        try
        {
            List<ProductItemViewModel> result = new();

            // Получение всех единиц продуктов и по ним продуктов
            var productItems = await _productItemRepository.GetAllAsync();
            var products = await _productRepository.GetByIdsAsync(productItems.Select(pi => pi.ProductId).Distinct());

            foreach (var productItem in productItems)
            {
                var product = products.FirstOrDefault(p => p.Id == productItem.ProductId);
                if (product is null)
                {
                    throw new EntityNotFoundException(nameof(Product), productItem.ProductId.ToString());
                }

                // Если кол-во продукта в холодильнике больше чем мин остаток на день то норм, иначе в выборку
                if (productItem.Amount > product.MinAmountPerDay) continue;

                result.Add(productItem.ToViewModel(product));
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetEndingProductItems", exception);
            throw;
        }
    }

    public async Task TakePartOfAsync(Guid productId, int count, Guid userId)
    {
        try
        {
            ProductId productIdEntity = ProductId.FromGuid(productId);
            UserId userIdEntity = UserId.FromGuid(userId);

            Product product = await _productRepository.FindByIdAsync(productIdEntity);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            // получаем все единицы продукта из холодильника, не просроченные
            var productItems = await _productItemRepository.GetByProductIdAsync(productIdEntity);
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
                    productItem.ReduceAmount(count, userIdEntity);
                    await _productItemRepository.ChangeAsync(productItem);
                    break;
                }
                else
                {
                    productItem.ReduceAmount(productItem.Amount, userIdEntity);
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

    public async Task WriteOffAsync(IEnumerable<Guid> productItemIds, Guid userId)
    {
        try
        {
            var productItemEntityIds = productItemIds.Select(ProductItemId.FromGuid);

            // получаем все указанные единицы продукта из холодильника
            var productItems = await _productItemRepository.GetByIdsAsync(productItemEntityIds);

            foreach (var productItem in productItems)
            {
                productItem.WriteOff(UserId.FromGuid(userId));
                await _productItemRepository.DeleteAsync(productItem);
            }
        }
        catch (Exception exception)
        {
            LogError("WriteOff", exception);
            throw;
        }
    }

    public async Task DeleteAsync(Guid productItemId)
    {
        try
        {
            var productItemEntityId = ProductItemId.FromGuid(productItemId);

            ProductItem productItem = await _productItemRepository.FindByIdAsync(productItemEntityId);

            if (productItem is null)
            {
                throw new EntityNotFoundException(nameof(ProductItem), productItemEntityId.ToString());
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
