using FoodStorage.Application.Services.RequestModels;
using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class ProductItemModelExtension
{
    public static ProductItemViewModel ToViewModel(this ProductItem productItem, Product product, Unit unit) =>
        new()
        {
            Id = productItem.Id.ToGuid(),
            Product = product.ToShortViewModel(),
            Amount = Math.Round(productItem.Amount * unit.Coefficient, 2), // конвертация в нужную единицу измерения
            Unit = unit.Id.ToString(),
            CreatingDate = productItem.CreatingDate,
            ExpiryDate = productItem.ExpiryDate
        };

    public static ProductItem ToEntity(this ProductItemCreateRequestModel productItemModel, UserId userId)
    {
        var productItemId = ProductItemId.CreateNew();
        var productId = ProductId.FromGuid(productItemModel.ProductId);
        // фэйковая дата окончания срока годности. При сохранении в бд она не учитывается, а в сущности она заполняется при поднятии из бд
        var expiryDate = productItemModel.CreatingDate.AddDays(1);

        return ProductItem.CreateNew(productItemId, productId, productItemModel.Amount, productItemModel.CreatingDate, expiryDate, userId);
    }
}
