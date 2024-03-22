using Domain.Entities.Exceptions;

namespace Domain.Entities.RecipeEntity;

/// <summary>
/// Наименование рецепта
/// </summary>
public record RecipeName
{
    private readonly string _value;

    private RecipeName(string value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static RecipeName FromString(string recipeName)
    {
        if (string.IsNullOrWhiteSpace(recipeName))
        {
            throw new DomainEntitiesException($"Передано пустое значение {nameof(RecipeName)}");
        }

        if (recipeName.Length is < 1 or > 250)
        {
            throw new DomainEntitiesException($"Некорректное значение параметра {nameof(recipeName)}: '{recipeName}'");
        }

        return new RecipeName(recipeName);
    }
}
