namespace FoodUserNotifier.Application.WebAPI.Models;

public sealed class DeliveryReportModel
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}
