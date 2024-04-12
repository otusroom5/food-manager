using FoodStorage.Domain.Entities.ProductEntity;

namespace Application.Repositories.Interface
{
    public interface IProductRepository
    {
       public string Create(Product product);
       public Product FindById(ProductId productId); 
       public Product FindByName(ProductName productName);
       public IEnumerable<Product> GetAll();
       public void Delete(Product product);
    }
}
