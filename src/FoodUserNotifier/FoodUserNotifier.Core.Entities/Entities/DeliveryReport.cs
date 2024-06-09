namespace FoodUserNotifier.Core.Entities;

public sealed class DeliveryReport
{
    public Guid Id { get; set; }
    public Guid NotificationId { get; set; }
    public string Message { get; set; }
    public bool Success {  get; set; }
}
