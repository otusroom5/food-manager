namespace FoodUserNotifier.Core.Entities;

public sealed class Report
{
    public Guid Id { get; set; }
    public Guid NotificationId { get; set; }
    public string Message { get; set; }
}
