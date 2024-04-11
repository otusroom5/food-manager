using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.RecipeEntity;

/// <summary>
/// Рецепт
/// </summary>
public record Recipe
{
    /// <summary>
    /// Идентификатор рецепта
    /// </summary>
    public RecipeId Id { get; init; }
    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public RecipeName Name { get; init; }

    private List<RecipePosition> _positions = new();
    /// <summary>
    /// Позиции рецепта
    /// </summary>
    public IReadOnlyCollection<RecipePosition> Positions => _positions.AsReadOnly();


    private Recipe(RecipeId id, RecipeName name)
    {
        Id = id;
        Name = name;
    }

    public static Recipe CreateNew(RecipeId id, RecipeName name, IEnumerable<RecipePosition> positions = null)
    {
        Recipe recipe = new(id, name);

        if (positions?.Any() == true)
        {
            foreach (var position in positions)
            {
                recipe.AddPosition(position);
            }
        }

        return recipe;
    }

    /// <summary>
    /// Добавление позиции в список рецепта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="count">Кол-во продукта</param>
    public void AddPosition(RecipePosition position)
    {
        bool isAlreadyExists = _positions.Select(p => p.ProductId).Contains(position.ProductId);
        if (isAlreadyExists)
        {
            throw new DomainEntitiesException($"Попытка добавить продукт '{position.ProductId}', который уже есть в рецепте");
        }

        _positions.Add(position);
    }

    /// <summary>
    /// Удаление позиции из списка рецепта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    public void RemovePosition(ProductId productId)
    {
        RecipePosition position = _positions.Find(p => p.ProductId == productId);
        if (position is null)
        {
            throw new DomainEntitiesException($"Попытка убрать из списка продукт '{productId}', которого нет в рецепте");
        }

        _positions.Remove(position);
    }
}
