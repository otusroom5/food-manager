using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductRepository
{
    public void Create(Product product);
    public Product FindById(ProductId productId);
    public Product FindByName(ProductName productName);
    public IEnumerable<Product> GetAll();
    public void Delete(Product product);
}