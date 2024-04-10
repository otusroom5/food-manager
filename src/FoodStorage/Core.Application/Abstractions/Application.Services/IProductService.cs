using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Services;

public interface IProductService
{
    public ProductId Create(Product product);
    public Product GetById(ProductId productId);
    public Product GetByName(ProductName productName);
    public IEnumerable<Product> GetAll();
    public void Delete(ProductId productId);

}
