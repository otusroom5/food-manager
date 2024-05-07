using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class ProductItemDtoExtension
{
    public static ProductItem ToEntity(this ProductItemDto productItemDto)
    {
        var productItemId = ProductItemId.FromGuid(productItemDto.Id);
        var productId = ProductId.FromGuid(productItemDto.ProductId);
        // Дата окончания срока годности = Дата изготовления + Срок годности в днях
        DateTime expiryDate = productItemDto.CreatingDate.AddDays(productItemDto.Product.BestBeforeDate);

        return ProductItem.CreateNew(productItemId, productId, productItemDto.Amount, productItemDto.CreatingDate, expiryDate);
    }

    public static ProductItemDto ToDto(this ProductItem productItem) =>
        new()
        {
            Id = productItem.Id.ToGuid(),
            ProductId = productItem.ProductId.ToGuid(),
            Amount = productItem.Amount,
            CreatingDate = productItem.CreatingDate
        };
}
