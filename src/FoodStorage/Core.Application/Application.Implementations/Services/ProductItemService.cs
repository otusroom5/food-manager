using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Implementations.Services;

public class ProductItemService : IProductItemService
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly IProductRepository _productRepository;

    public ProductItemService(IProductItemRepository productItemRepository,
        IProductRepository productRepository)
    {
        _productItemRepository = productItemRepository;
        _productRepository = productRepository;
    }

    public ProductItemId Create(ProductItem productItem)
    {
        // проверка существования продукта, единицу которого хотим положить в холодильник
        Product product = _productRepository.FindById(productItem.ProductId);

        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productItem.ProductId.ToString());
        }

        _productItemRepository.CreateAsync(productItem);

        return productItem.Id;
    }

    public ProductItem GetById(ProductItemId productItemId)
    {
        ProductItem result = _productItemRepository.FindById(productItemId);

        if (result is null)
        {
            throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
        }

        return result;
    }

    public IEnumerable<ProductItem> GetByProductId(ProductId productId) => _productItemRepository.GetByProductId(productId);

    public IEnumerable<ProductItem> GetAll() => _productItemRepository.GetAll();

    public IEnumerable<ProductItem> GetExpireProductItems(int daysBeforeExpired = 0)
    {
        var result = _productItemRepository.GetAll();

        return result.Where(r => r.ExpiryDate.AddDays(-daysBeforeExpired).Date <= DateTime.UtcNow.Date);
    }

    public void TakePartOf(ProductId productId, int count, UserId userId)
    {
        Product product = _productRepository.FindById(productId);

        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        // получаем все единицы продукта из холодильника, не просроченные
        var productItems = _productItemRepository.GetByProductId(productId)
                                                 .Where(pi => pi.ExpiryDate.Date > DateTime.UtcNow.Date);

        // общее кол-во продукта в холодильнике
        int commonCount = productItems.Sum(pi => pi.Amount);

        // если общее кол-во продукта в холодильнике меньше запрашиваемого - ошибка
        if (commonCount < count)
        {
            throw new ApplicationLayerException($"Общее кол-во продукта {product.Name} в холодильнике меньше запрашиваемого ({count})");
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
                _productItemRepository.ChangeAsync(productItem);
                break;
            }
            else
            {
                productItem.ReduceAmount(productItem.Amount, userId);
                _productItemRepository.DeleteAsync(productItem);
                count -= productItem.Amount;
            }
        }
    }

    public void WriteOff(IEnumerable<ProductItemId> productItemIds, UserId userId)
    {
        // получаем все указанные единицы продукта из холодильника
        var productItems = _productItemRepository.GetByIds(productItemIds);

        foreach (var productItem in productItems)
        {
            productItem.WriteOff(userId);
            _productItemRepository.DeleteAsync(productItem);
        }
    }

    public void Delete(ProductItemId productItemId)
    {
        ProductItem productItem = _productItemRepository.FindById(productItemId);

        if (productItem is null)
        {
            throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
        }

        _productItemRepository.DeleteAsync(productItem);
    }
}
