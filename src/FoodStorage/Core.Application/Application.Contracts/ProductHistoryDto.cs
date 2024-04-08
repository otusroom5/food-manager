using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Contracts
{
    /// <summary>
    /// Дто история хранения продуктов
    /// </summary>
    public class ProductHistoryDto
    {
        /// <summary>
        /// Идентификатор истории продукта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public Guid ProductId { get; set; }       

        /// <summary>
        /// Кто провел действие с продуктом
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Когда было проведено действие с продуктом
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Действие с продуктом (статус продукта в истории)
        /// </summary>
        public ProductState State { get; set; }

        /// <summary>
        /// Количество продукта
        /// </summary>
        public int Count { get; set; }
    }
}
