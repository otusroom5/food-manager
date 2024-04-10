﻿using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;

namespace FoodStorage.WebApi.Models.Extensions;

public static class ProductItemModelExtension
{
    public static ProductItem ToEntity(this ProductItemModel productItemModel)
    {
        var productItemId = ProductItemId.FromGuid(productItemModel.Id);
        var productId = ProductId.FromGuid(productItemModel.ProductId);
        // фэйковая дата окончания срока годности. При сохранении в бд она не учитывается, а в сущности она заполняется при поднятии из бд
        var expiryDate = productItemModel.CreatingDate.AddDays(1);

        return ProductItem.CreateNew(productItemId, productId, productItemModel.Amount, productItemModel.CreatingDate, expiryDate);
    }

    public static ProductItemModel ToModel(this ProductItem productItem) =>
        new()
        {
            Id = productItem.Id.ToGuid(),
            ProductId = productItem.ProductId.ToGuid(),
            Amount = productItem.Amount,
            CreatingDate = productItem.CreatingDate, 
            ExpiryDate = productItem.ExpiryDate
        };
}
