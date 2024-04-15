using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Repositories
{
    public interface IProductItemRepository
    {
        public ProductItemId Create(ProductItem productItem);
        public ProductItem FinfById(ProductItemId productItemId);
        public IEnumerable<ProductItem> GetByProductName(ProductName productName);
        public IEnumerable<ProductItem> GetAll();
        public void Change(ProductItem productItem);
        public void Delete(ProductItem productItem);
    }
}
