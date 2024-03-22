using FoodStorage.Domain.Entities.Common;

namespace FoodStorage.Domain.Entities.RecipeEntity;

/// <summary>
/// Идентификатор рецепта
/// </summary>
public record RecipeId
{
    private readonly Guid _value;

    private RecipeId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static RecipeId FromGuid(Guid value)
    {
        value.ValidateOrThrow(nameof(RecipeId));
        return new RecipeId(value);
    }

    public static RecipeId CreateNew() => new RecipeId(Guid.NewGuid());
}
