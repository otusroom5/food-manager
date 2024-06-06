using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Implementations.Common.Extensions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
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

    public async Task<Guid> CreateAsync(ProductCreateRequestModel product)
    {
        try
        {
            Product productEntity = product.ToEntity();

            // проверка на существование продукта с таким же наименованием
            Product productWithSameName = await _productRepository.FindByNameAsync(productEntity.Name);
            if (productWithSameName is not null)
            {
                throw new ApplicationLayerException($"{nameof(Product)} with same name '{productEntity.Name}' is already exists");
            }

            await _productRepository.CreateAsync(productEntity);

            return productEntity.Id.ToGuid();
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<ProductViewModel> GetByIdAsync(Guid productId)
    {
        try
        {
            var productEntityId = ProductId.FromGuid(productId);

            Product product = await _productRepository.FindByIdAsync(productEntityId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productEntityId.ToString());
            }

            return product.ToViewModel();
        }
        catch (Exception exception)
        {
            LogError("GetById", exception);
            throw;
        }
    }

    public async Task<ProductViewModel> GetByNameAsync(string productName)
    {
        try
        {
            Product product = await _productRepository.FindByNameAsync(ProductName.FromString(productName));

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productName);
            }

            return product.ToViewModel();
        }
        catch (Exception exception)
        {
            LogError("GetByName", exception);
            throw;
        }
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => p.ToViewModel()).ToList();
        }
        catch (Exception exception)
        {
            LogError("GetAll", exception);
            throw;
        }
    }

    public async Task DeleteAsync(Guid productId)
    {
        try
        {
            var productEntityId = ProductId.FromGuid(productId);

            Product product = await _productRepository.FindByIdAsync(productEntityId);

            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productEntityId.ToString());
            }

            await _productRepository.DeleteAsync(product);
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
