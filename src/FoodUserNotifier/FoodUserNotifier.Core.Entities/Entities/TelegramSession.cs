namespace FoodUserNotifier.Core.Entities.Entities;

public sealed class TelegramSession
{
    public Guid Id { get; set; }
    public Guid RecepientId { get; set; }
    public long ChatId { get; set; }
}
