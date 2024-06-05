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

    private readonly List<RecipePosition> _positions = new();
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
            throw new DomainEntitiesException($"Trying to add product '{position.ProductId}' that is already in the recipe");
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
        // Попытка убрать из списка продукт, которого нет в рецепте
        if (position is null)
        {
            throw new DomainEntitiesException($"An attempt to remove a product '{productId}' from the list that is not in the recipe");
        }

        _positions.Remove(position);
    }
}
