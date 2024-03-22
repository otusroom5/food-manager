using Domain.Entities.Exceptions;
using Domain.Entities.ProductEntity;

namespace Domain.Entities.RecipeEntity;

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


    public Recipe(Guid id, string name)
    {
        Id = RecipeId.FromGuid(id);
        Name = RecipeName.FromString(name);
    }

    public Recipe(Guid id, string name, Dictionary<Guid, int> positions) : this(id, name)
    {
        foreach (var position in positions)
        {
            var recipePosition = new RecipePosition(position.Key, position.Value);
            AddPosition(recipePosition);
        }
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
