using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Application.Services;

public interface IProductItemService
{
    public ProductItemId Create(ProductItem productItem);
    public ProductItem GetById(ProductItemId productItemId);
    public IEnumerable<ProductItem> GetByProductId(ProductId productId);
    public IEnumerable<ProductItem> GetAll();
    public IEnumerable<ProductItem> GetExpireProductItems(int daysBeforeExpired = 0);
    public void TakePartOf(ProductId productId, int count, UserId userId);
    public void WriteOff(IEnumerable<ProductItemId> productItemIds, UserId userId);
    public void Delete(ProductItemId productItemId);
}
