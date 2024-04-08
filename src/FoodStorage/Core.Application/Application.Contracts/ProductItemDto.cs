namespace FoodStorage.Application.Contracts
{
    /// <summary>
    /// Дто единицы продукта (в холодильнике)
    /// </summary>
    public class ProductItemDto
    {
        /// <summary>
        /// Идентификатор единицы продукта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество продукта в холодильнике
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Дата изготовления
        /// </summary>
        public DateTime CreatingDate { get; set; }
    }
}
