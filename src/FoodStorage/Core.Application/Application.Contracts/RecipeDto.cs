using FoodStorage.Domain.Entities.RecipeEntity;

namespace FoodStorage.Application.Contracts
{
    /// <summary>
    /// Дто рецепта
    /// </summary>
    public class RecipeDto
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование рецепта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Позиции рецепта
        /// </summary>
        public IReadOnlyCollection<RecipePosition> Positions { get; set; } = new List<RecipePosition>();
    }
}
