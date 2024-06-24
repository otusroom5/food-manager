using FoodStorage.Application.Services.ViewModels;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Application.Implementations.Common.Extensions;

public static class ProductHistoryModelExtension
{
    public static ProductHistoryViewModel ToViewModel(this ProductHistory productHistory, Product product, Unit unit) =>
        new()
        {
            Product = product.ToShortViewModel(),
            Count = productHistory.Count,
            Unit = unit.Id.ToString(),
            State = productHistory.State.ToString(),
            CreatedBy = productHistory.CreatedBy.ToGuid(),
            CreatedAt = productHistory.CreatedAt
        };
}
