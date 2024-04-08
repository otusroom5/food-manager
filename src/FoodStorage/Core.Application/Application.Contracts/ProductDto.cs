﻿using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Application.Contracts
{
    /// <summary>
    /// ДТО продукта
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Единица изменерения
        /// </summary>
        public ProductUnit Unit { get; set; }

        /// <summary>
        /// Минимальный остаток на день
        /// </summary>
        public int MinAmountPerDay { get; set; }

        /// <summary>
        /// Срок годности в часах
        /// </summary>
        public double BestBeforeDate { get; set; }
    }
}
