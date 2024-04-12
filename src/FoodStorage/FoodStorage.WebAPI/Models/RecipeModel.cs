namespace FoodStorage.WebApi.Models;

/// <summary>
/// Модель рецепта
/// </summary>
public class RecipeModel
{  
   /// <summary>
   /// Идентификатор рецепта
   /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Позиции рецепта (продукт + его количество)
    /// </summary>
    public Dictionary<Guid, string> Positions { get; set; } = new Dictionary<Guid, string>();
}
