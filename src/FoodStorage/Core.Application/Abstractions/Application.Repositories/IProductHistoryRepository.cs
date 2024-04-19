using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductHistoryRepository
{
    public ProductHistoryId Create(ProductHistory productHistory);
    public ProductHistory FindById(ProductHistory productHistory);
    public IEnumerable<ProductHistory> GetByProductId(ProductId productId);
    public IEnumerable<ProductHistory> GetAll();
    public void Delete(ProductHistory productHistory);
}