using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.ProductEntity;


namespace Application.Repositories.Interface
{
    public interface IProductHistoryRepository
    {
       public ProductHistoryId Create(ProductHistory productHistory);
       public ProductHistory FindById(ProductHistory productHistory);
       public IEnumerable<ProductHistory> GetByProductName(ProductName productName);
       public IEnumerable<ProductHistory> GetAll();
       public void Delete(ProductHistory productHistory);
    }
}
