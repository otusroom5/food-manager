﻿namespace FoodStorage.Application.Services.RequestModels;

/// <summary>
/// Модель запроса позиции рецепта
/// </summary>
public sealed record RecipePositionRequestModel
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid ProductId { get; set; }
    /// <summary>
    /// Количество продукта
    /// </summary>
    public int ProductCount { get; set; }
}
