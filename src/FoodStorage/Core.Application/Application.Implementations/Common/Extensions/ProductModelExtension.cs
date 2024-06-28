using FoodStorage.Application.Implementations.Common.Exceptions;
using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class ProductModelExtension
{
    public static ProductViewModel ToViewModel(this Product product) =>
        new()
        {
            Id = product.Id.ToGuid(),
            Name = product.Name.ToString(),
            UnitType = product.UnitType.ToString(),
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
        if (!Enum.TryParse<UnitType>(productModel.UnitType, true, out var unitType))
        {
            throw new InvalidEnumValueException(nameof(productModel.UnitType), productModel.UnitType, nameof(UnitType));
        }

        return Product.CreateNew(ProductId.CreateNew(), ProductName.FromString(productModel.Name), unitType,
            productModel.MinAmountPerDay, productModel.BestBeforeDate);
    }
}
