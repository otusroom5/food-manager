using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.RecipeEntity;

/// <summary>
/// Позиция в рецепте: продукт, количество продукта
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
    public int ProductCount { get; init; }

    private RecipePosition() { }

    public static RecipePosition CreateNew(ProductId productId, int productCount)
    {
        if (productCount <= 0)
        {
            throw new InvalidArgumentValueException("Количество продукта в рецепте должно быть больше 0", nameof(ProductCount));
        }

        return new()
        {
            ProductId = productId,
            ProductCount = productCount
        };
    }
}
