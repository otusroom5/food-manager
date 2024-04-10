using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Implementations;

public class ProductService : IProductService
{
    public ProductId Create(Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProductId productId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(ProductId productId)
    {
        throw new NotImplementedException();
    }

    public Product GetByName(ProductName productName)
    {
        throw new NotImplementedException();
    }
}
