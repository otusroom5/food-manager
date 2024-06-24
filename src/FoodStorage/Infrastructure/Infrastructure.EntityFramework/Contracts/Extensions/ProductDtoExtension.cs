using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.UnitEntity;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class ProductDtoExtension
{
    public static Product ToEntity(this ProductDto productDto)
    {
        if (!Enum.TryParse<UnitTypeE>(productDto.UnitType, out var unit))
        {
            throw new InvalidEnumValueException(nameof(productDto.UnitType), productDto.UnitType, nameof(UnitTypeE));
        }

        return Product.CreateNew(ProductId.FromGuid(productDto.Id), ProductName.FromString(productDto.Name), unit, productDto.MinAmountPerDay, productDto.BestBeforeDate);
    }

    public static ProductDto ToDto(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString(),
            UnitType = product.UnitType.ToString(),
            MinAmountPerDay = product.MinAmountPerDay,
            BestBeforeDate = product.BestBeforeDate
        };
}
