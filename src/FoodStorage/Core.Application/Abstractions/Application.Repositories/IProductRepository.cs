using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductRepository
{
    public Task CreateAsync(Product product);
    public Task<Product> FindByIdAsync(ProductId productId);
    public Task<Product> FindByNameAsync(ProductName productName);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task DeleteAsync(Product product);
}