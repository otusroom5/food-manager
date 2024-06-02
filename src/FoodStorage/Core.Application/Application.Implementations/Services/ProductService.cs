using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public ProductId Create(Product product)
    {
        try
        {
            // проверка на существование продукта с таким же наименованием
            Product productWithSameName = _productRepository.FindByName(product.Name);
            if (productWithSameName is not null)
            {
                throw new ApplicationLayerException($"{nameof(Product)} with same name '{product.Name}' is already exists");
            }

            _productRepository.Create(product);

            return product.Id;
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public Product GetById(ProductId productId)
    {
        try
        {
            Product result = _productRepository.FindById(productId);

            if (result is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public Product GetByName(ProductName productName)
    {
        try
        {
            Product result = _productRepository.FindByName(productName);

            if (result is null)
            {
                throw new EntityNotFoundException(nameof(Product), productName.ToString());
            }

            return result;
        }
        catch (Exception exception)
        {
            LogError("GetByName", exception);
            throw;
        }
    }

    public IEnumerable<Product> GetAll() => _productRepository.GetAll();

    public void Delete(ProductId productId)
    {
        try
        {
            Product product = _productRepository.FindById(productId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId.ToString());
            }

            _productRepository.Delete(product);
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
