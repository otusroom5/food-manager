﻿using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Implementations;

public class ProductItemService : IProductItemService
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;

    public ProductItemService(IProductItemRepository productItemRepository, 
        IProductRepository productRepository, 
        IProductHistoryRepository productHistoryRepository)
    {
        _productItemRepository = productItemRepository;
        _productRepository = productRepository;
        _productHistoryRepository = productHistoryRepository;
    }

    public ProductItemId Create(ProductItem productItem)
    {
        // проверка существования продукта, единицу которого хотим положить в холодильник
        Product product = _productRepository.FindById(productItem.ProductId);

        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productItem.ProductId.ToString());
        }

        _productItemRepository.Create(productItem);

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

    public IEnumerable<ProductItem> GetExpiredProductItems()
    {
        var result = _productItemRepository.GetAll();

        return result.Where(r => r.ExpiryDate < DateTime.UtcNow);
    }

    public void TakePartOf(ProductId productId, int count)
    {
        Product product = _productRepository.FindById(productId);

        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        // получаем все единицы продукта из холодильника, не просроченные
        var productItems = _productItemRepository.GetByProductId(productId)
                                                 .Where(pi => pi.ExpiryDate > DateTime.UtcNow);

        // общее кол-во продукта в холодильнике
        int commonCount = productItems.Sum(pi => pi.Amount);

        // если общее кол-во продукта в холодильнике меньше запрашиваемого - ошибка
        if (commonCount - product.MinAmountPerDay < count)
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
            if (productItem.Amount >= count)
            {
                productItem.ReduceAmount(count);
                break;
            }
            else
            {
                productItem.ReduceAmount(productItem.Amount);
                count -= productItem.Amount;
            }
        }
    }

    public void WriteOff(IEnumerable<ProductItemId> productItemIds)
    {
        // получаем все указанные единицы продукта из холодильника
        var productItems = _productItemRepository.GetByIds(productItemIds);

        // формируем словарь Продукт - количество продукта, для записи в историю
        Dictionary<ProductId, int> productCountDict = new Dictionary<ProductId, int>();

        foreach (var productItem in productItems)
        {
            if (productCountDict.ContainsKey(productItem.ProductId))
            {
                productCountDict[productItem.ProductId] += productItem.Amount;
            }
            else
            {
                productCountDict.Add(productItem.ProductId, productItem.Amount);
            }

            // Удаление продукта
            _productItemRepository.Delete(productItem);
        }

        // запись в историю о списании продукта
        foreach (var productCountItem in productCountDict)
        {
            ProductHistory productHistoryItem = ProductHistory.CreateNew(
                id: ProductHistoryId.CreateNew(),
                productId: productCountItem.Key,
                state: ProductState.WriteOff,
                count: productCountItem.Value,
                createdBy: UserId.FromGuid(Guid.NewGuid()),
                createdAt: DateTime.UtcNow);

            _productHistoryRepository.Create(productHistoryItem);
        }
    }

    public void Delete(ProductItemId productItemId)
    {
        ProductItem productItem = _productItemRepository.FindById(productItemId);

        if (productItem is null)
        {
            throw new EntityNotFoundException(nameof(ProductItem), productItemId.ToString());
        }

        _productItemRepository.Delete(productItem);
    }
}