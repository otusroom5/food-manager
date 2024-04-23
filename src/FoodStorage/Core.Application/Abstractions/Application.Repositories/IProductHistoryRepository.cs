using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductHistoryRepository
{
    public void Create(ProductHistory productHistory);
    public ProductHistory FindById(ProductHistoryId productHistoryId);
    public IEnumerable<ProductHistory> GetByProductId(ProductId productId);
    public IEnumerable<ProductHistory> GetAll();
    public void Delete(ProductHistory productHistory);
}