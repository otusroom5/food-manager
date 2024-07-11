using FoodManager.WebUI.Areas.Cooker.Contracts;
using FoodManager.WebUI.Areas.Cooker.Models;

namespace FoodManager.WebUI.Extensions;

public static class ProductExtensions
{
    public static ProductModel ToModel(this Product product)
        => new ProductModel()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            UnitType = product.UnitType,
            BestBeforeDate = product.BestBeforeDate.ToString(),
            MinAmountPerDay = product.MinAmountPerDay.ToString()
        };

    public static ProductItemModel ToModel(this ProductItem productItem)
        => new ProductItemModel()
        {
            Id = productItem.Id.ToString(),
            ProductId = productItem.Product.Id.ToString(),
            ProductName = productItem.Product.Name,
            Amount = productItem.Amount.ToString(),
            UnitId = productItem.Unit,
            CreatingDate = productItem.CreatingDate,
            ExpiryDate = productItem.ExpiryDate
        };

    public static ProductHistoryModel ToModel(this ProductHistory productHistory)
        => new ProductHistoryModel()
        {
            ProductId = productHistory.Product.Id.ToString(),
            ProductName = productHistory.Product.Name,
            Action = productHistory.State,
            Count = productHistory.Count.ToString(),
            Unit = productHistory.Unit,
            CreatedAt = productHistory.CreatedAt.ToString()
        };
}
