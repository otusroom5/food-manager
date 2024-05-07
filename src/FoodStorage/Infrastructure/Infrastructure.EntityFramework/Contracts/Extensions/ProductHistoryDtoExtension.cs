using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

public static class ProductHistoryDtoExtension
{
    public static ProductHistory ToEntity(this ProductHistoryDto productHistoryDto)
    {
        var productHistoryId = ProductHistoryId.FromGuid(productHistoryDto.Id);
        var productId = ProductId.FromGuid(productHistoryDto.ProductId);
        var userId = UserId.FromGuid(productHistoryDto.CreatedBy);

        if (!Enum.TryParse<ProductState>(productHistoryDto.State, out var state))
        {
            throw new InvalidEnumValueException(nameof(productHistoryDto.State), productHistoryDto.State, nameof(ProductState));
        }

        return ProductHistory.CreateNew(productHistoryId, productId, state, productHistoryDto.Count, userId, productHistoryDto.CreatedAt);
    }

    public static ProductHistoryDto ToDto(this ProductHistory productHistory) =>
        new()
        {
            Id = productHistory.Id.ToGuid(),
            ProductId = productHistory.ProductId.ToGuid(),
            State = productHistory.State.ToString(),
            Count = productHistory.Count,
            CreatedBy = productHistory.CreatedBy.ToGuid(),
            CreatedAt = productHistory.CreatedAt
        };
}
