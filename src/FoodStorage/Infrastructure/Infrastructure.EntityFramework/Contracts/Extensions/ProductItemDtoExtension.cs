using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class ProductItemDtoExtension
{
    public static ProductItem ToEntity(this ProductItemDto productItemDto)
    {
        var productItemId = ProductItemId.FromGuid(productItemDto.Id);
        var productId = ProductId.FromGuid(productItemDto.ProductId);
        var amount = Math.Round(productItemDto.Amount, 2);
        // Дата окончания срока годности = Дата изготовления + Срок годности в днях
        DateTime expiryDate = productItemDto.CreatingDate.AddDays(productItemDto.Product.BestBeforeDate);

        return ProductItem.CreateNew(productItemId, productId, amount, productItemDto.CreatingDate, expiryDate);
    }

    public static ProductItemDto ToDto(this ProductItem productItem) =>
        new()
        {
            Id = productItem.Id.ToGuid(),
            ProductId = productItem.ProductId.ToGuid(),
            Amount = Math.Round(productItem.Amount, 2),
            CreatingDate = productItem.CreatingDate.Date
        };
}
