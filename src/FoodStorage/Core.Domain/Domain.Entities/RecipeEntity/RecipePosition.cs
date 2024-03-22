using Domain.Entities.Exceptions;
using Domain.Entities.ProductEntity;

namespace Domain.Entities.RecipeEntity;

/// <summary>
/// Позиция в рецепте: продукт, количество продукта
/// </summary>
public record RecipePosition
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public ProductId ProductId { get; init; }

    private int _productCount;
    /// <summary>
    /// Количество продукта
    /// </summary>
    public int ProductCount 
    { 
        get => _productCount;
        init
        {
            if (value <= 0)
            {
                throw new DomainEntitiesException("Количество продукта в рецепте должно быть больше 0", nameof(RecipePosition), nameof(ProductCount));
            }
            _productCount = value;
        }
    }

    public RecipePosition(Guid productId, int productCount)
    {
        ProductId = ProductId.FromGuid(productId);
        ProductCount = productCount;
    }
}
