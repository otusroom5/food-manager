using FoodStorage.Domain.Entities.Common.Exceptions;

namespace FoodStorage.Domain.Entities.RecipeEntity;

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
            throw new InvalidArgumentValueException("Передано пустое значение", nameof(RecipeName));
        }

        if (recipeName.Length is < 2 or > 100)
        {
            throw new InvalidArgumentValueException("Передано некорректное значение", nameof(RecipeName));
        }

        return new RecipeName(recipeName);
    }
}
