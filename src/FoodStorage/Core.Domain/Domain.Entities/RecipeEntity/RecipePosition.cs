using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.UnitEntity;

namespace FoodStorage.Domain.Entities.RecipeEntity;

/// <summary>
/// Позиция в рецепте: продукт, количество продукта, единица измерения
/// </summary>
public record RecipePosition
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId ProductId { get; init; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public double ProductCount { get; init; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public UnitId UnitId { get; init; }

    private RecipePosition() { }

    public static RecipePosition CreateNew(ProductId productId, double productCount, UnitId unitId)
    {
        if (productCount <= 0)
        {
            throw new InvalidArgumentValueException("The amount of product in the recipe must be greater than 0", nameof(ProductCount));
        }

        return new()
        {
            ProductId = productId,
            ProductCount = productCount,
            UnitId = unitId
        };
    }
}
