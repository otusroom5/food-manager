using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.WebApi.Common.Exceptions;
using FoodStorage.WebApi.Models.ProductModels;

namespace FoodStorage.WebApi.Models.Extensions;

public static class ProductModelExtension
{
    public static Product ToEntity(this CreateProductModel productModel)
    {
        if (!Enum.TryParse<ProductUnit>(productModel.Unit, true, out var unit))
        {
            throw new InvalidEnumValueException(nameof(productModel.Unit), productModel.Unit, nameof(ProductUnit));
        }

        return Product.CreateNew(ProductId.CreateNew(), ProductName.FromString(productModel.Name), unit, 
            productModel.MinAmountPerDay, productModel.BestBeforeDate);
    }

    public static ProductModel ToModel(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString(),
            Unit = product.Unit.ToString(),
            MinAmountPerDay = product.MinAmountPerDay,
            BestBeforeDate = product.BestBeforeDate
        };
}
