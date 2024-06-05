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

    public async Task CreateAsync(Product product)
    {
        if (product is null)
        {
            throw new EmptyArgumentValueException(nameof(product));
        }

        ProductDto productDto = product.ToDto();
        _databaseContext.Products.Add(productDto);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<Product> FindByIdAsync(ProductId productId)
    {
        ProductDto productDto = await _databaseContext.Products.FindAsync(productId.ToGuid());
        return productDto is null ? null : productDto.ToEntity();
    }

    public async Task<Product> FindByNameAsync(ProductName productName)
    {
        ProductDto productDto = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == productName.ToString().ToLower());
        return productDto is null ? null : productDto.ToEntity();
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _databaseContext.Products.Select(p => p.ToEntity()).ToListAsync();

    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<ProductId> productIds)
    {
        var guidIds = productIds.Select(pi => pi.ToGuid());

        IEnumerable<ProductDto> productDtos = await _databaseContext.Products.Where(pi => guidIds.Contains(pi.Id)).ToListAsync();
        return productDtos.Select(pi => pi.ToEntity()).ToList();
    }

    public async Task DeleteAsync(Product product)
    {
        if (product is null)
        {
            throw new EmptyArgumentValueException(nameof(product));
        }

        ProductDto productDto = product.ToDto();
        _databaseContext.Products.Remove(productDto);

        await _databaseContext.SaveChangesAsync();
    }
}
