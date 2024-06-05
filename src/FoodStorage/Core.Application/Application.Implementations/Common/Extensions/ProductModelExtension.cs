using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class ProductModelExtension
{
    public static ProductViewModel ToViewModel(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString(),
            Unit = product.Unit.ToString(),
            MinAmountPerDay = product.MinAmountPerDay,
            BestBeforeDate = product.BestBeforeDate
        };

    public static ProductShortViewModel ToShortViewModel(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString()
        };

    public static Product ToEntity(this ProductCreateRequestModel productModel)
    {
        if (!Enum.TryParse<ProductUnit>(productModel.Unit, true, out var unit))
        {
            throw new InvalidEnumValueException(nameof(productModel.Unit), productModel.Unit, nameof(ProductUnit));
        }

        return Product.CreateNew(ProductId.CreateNew(), ProductName.FromString(productModel.Name), unit,
            productModel.MinAmountPerDay, productModel.BestBeforeDate);
    }
}
