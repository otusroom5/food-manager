using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class ProductDtoExtension
{
    public static Product ToEntity(this ProductDto productDto)
    {
        if (Enum.TryParse<ProductUnit>(productDto.Unit, out var unit))
        {
            throw new InvalidEnumValueException(nameof(productDto.Unit), productDto.Unit, nameof(ProductUnit));
        }

        return Product.CreateNew(ProductId.FromGuid(productDto.Id), ProductName.FromString(productDto.Name), unit, productDto.MinAmountPerDay, productDto.BestBeforeDate);
    }

    public static ProductDto ToDto(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString(),
            Unit = product.Unit.ToString(),
            MinAmountPerDay = product.MinAmountPerDay,
            BestBeforeDate = product.BestBeforeDate
        };
}
