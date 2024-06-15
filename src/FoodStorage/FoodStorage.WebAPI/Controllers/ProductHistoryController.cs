﻿using FoodManager.Shared.Types;
using FoodStorage.Application.Services;
using FoodStorage.Application.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStorage.WebApi.Controllers;

[Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Cooker)]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ProductHistoryController : BaseController
{
    private readonly IProductHistoryService _productHistoryService;

    public ProductHistoryController(IProductHistoryService productHistoryService)
    {
        _productHistoryService = productHistoryService;
    }

    /// <summary>
    /// Вывести все продукты с определенным статусом в интервале дат
    /// </summary>
    /// <param name="state">Статус продукта</param>
    /// <param name="dateStart">Начало интервала</param>
    /// <param name="dateEnd">Конец интервала</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetProductsByStateInDateInterval")]
    public async Task<ActionResult<List<ProductHistoryViewModel>>> GetProductsByStateInDateIntervalAsync(string state, DateTime dateStart, DateTime dateEnd)
    {
        List<ProductHistoryViewModel> result = await _productHistoryService.GetProductsByStateInDateIntervalAsync(state, dateStart, dateEnd);

        return Ok(result);
    }

    /// <summary>
    /// Поcмотреть действия с продуктом за определенный день
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="date">Дата</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetActionsWithProductByDate")]
    public async Task<ActionResult<List<ProductHistoryViewModel>>> GetActionsWithProductByDateAsync(Guid productId, DateTime date)
    {
        List<ProductHistoryViewModel> result = await _productHistoryService.GetActionsWithProductByDateAsync(productId, date);

        return Ok(result);
    }

    /// <summary>
    /// Поcмотреть действия с продуктами конкретным пользователем за определенный день
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="date">День</param>
    /// <returns>Продукт</returns>
    [HttpGet("GetActionsWithProductByUserInDate")]
    public async Task<ActionResult<List<ProductHistoryViewModel>>> GetActionsWithProductByUserInDateAsync(Guid userId, DateTime date)
    {
        List<ProductHistoryViewModel> result = await _productHistoryService.GetActionsWithProductByUserInDateAsync(userId, date);

        return Ok(result);
    }
}
