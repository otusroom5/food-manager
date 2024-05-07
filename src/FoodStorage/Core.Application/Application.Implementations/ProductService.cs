using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Repositories;
using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductId Create(Product product)
    {
        // проверка на существование продукта с таким же наименованием
        Product productWithSameName = _productRepository.FindByName(product.Name);
        if (productWithSameName is not null)
        {
            throw new ApplicationLayerException($"Уже существует продукт с таким наименованием: {product.Name}");
        }

        _productRepository.Create(product);

        return product.Id;
    }

    public Product GetById(ProductId productId)
    {
        Product result = _productRepository.FindById(productId);

        if (result is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        return result;
    }

    public Product GetByName(ProductName productName)
    {
        Product result = _productRepository.FindByName(productName);

        if (result is null)
        {
            throw new EntityNotFoundException(nameof(Product), productName.ToString());
        }

        return result;
    }

    public IEnumerable<Product> GetAll() => _productRepository.GetAll();

    public void Delete(ProductId productId)
    {
        Product product = _productRepository.FindById(productId);

        if (product is null)
        {
            throw new EntityNotFoundException(nameof(Product), productId.ToString());
        }

        _productRepository.Delete(product);
    }
}
