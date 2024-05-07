using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.Implementations;

internal class ProductRepository : IProductRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Create(Product product)
    {
        if (product is null)
        {
            throw new EmptyArgumentValueException(nameof(product));
        }

        ProductDto productDto = product.ToDto();
        _databaseContext.Products.Add(productDto);

        _databaseContext.SaveChanges();
    }

    public Product FindById(ProductId productId)
    {
        ProductDto productDto = _databaseContext.Products.FirstOrDefault(p => p.Id == productId.ToGuid());
        return productDto is null ? null : productDto.ToEntity();
    }

    public Product FindByName(ProductName productName)
    {
        ProductDto productDto = _databaseContext.Products.FirstOrDefault(p => p.Name.ToLower() == productName.ToString().ToLower());
        return productDto is null ? null : productDto.ToEntity();
    }

    public IEnumerable<Product> GetAll() => _databaseContext.Products.Select(p => p.ToEntity()).ToList();

    public void Delete(Product product)
    {
        if (product is null)
        {
            throw new EmptyArgumentValueException(nameof(product));
        }

        ProductDto productDto = product.ToDto();
        _databaseContext.Products.Remove(productDto);

        _databaseContext.SaveChanges();
    }
}
