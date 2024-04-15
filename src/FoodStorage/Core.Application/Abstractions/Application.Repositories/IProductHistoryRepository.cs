using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Repositories
{
    public interface IProductHistoryRepository
    {
        public ProductHistoryId Create(ProductHistory productHistory);
        public ProductHistory FindById(ProductHistory productHistory);
        public IEnumerable<ProductHistory> GetByProductId(ProductId productId);
        public IEnumerable<ProductHistory> GetAll();
        public void Delete(ProductHistory productHistory);
    }
}
