using Domain.Entities.Exceptions;

namespace Domain.Entities.RecipeEntity;

/// <summary>
/// Идентификатор рецепта
/// </summary>
public record RecipeId
{
    private readonly Guid _value;

    private RecipeId(Guid recipeId)
    {
        if (recipeId != Guid.Empty)
        {
            throw new DomainEntitiesException($"Передано некорректное значение {nameof(RecipeId)}");
        }

        _value = recipeId;
    }

    public Guid ToGuid() => _value;
    public static RecipeId FromGuid(Guid value) => new RecipeId(value);

    public static RecipeId CreateNew() => new RecipeId(Guid.NewGuid());
}
