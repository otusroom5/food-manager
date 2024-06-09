using FoodManager.Shared.Types;
using FoodUserNotifier.Application.WebAPI.Contracts;
using FoodUserNotifier.Application.WebAPI.Models;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodUserNotifier.Application.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Administration)]
public class DeliveryReportsController : ControllerBase
{
    private readonly IDeliveryReportsRepository _deliveryReportsRepository;
    public DeliveryReportsController(IUnitOfWork unitOfWork) 
    {
        _deliveryReportsRepository = unitOfWork.GetDeliveryReportsRepository();
    }

    [HttpGet("GetByNotificationId")]
    public async Task<IActionResult> GetByNotificationId(Guid notificationId)
    {
        DeliveryReport report = await _deliveryReportsRepository.FindByNotificationIdAsync(notificationId);
        
        if (report == null)
        {
            return BadRequest(ResponseBase.Create("Delivery report is not found"));
        }

        return Ok(new DeliveryReportModel()
        {
            Id = report.Id,
            Message = report.Message,
            Success = report.Success
        });
    }

}
