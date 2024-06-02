using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Services;

public interface IProductService
{
    public Task<ProductId> CreateAsync(Product product);
    public Task<Product> GetByIdAsync(ProductId productId);
    public Task<Product> GetByNameAsync(ProductName productName);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task DeleteAsync(ProductId productId);

}
