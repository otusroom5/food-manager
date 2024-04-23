using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.WebApi.Models;

public class RecepientModel
{
    public Guid Id { get; set; }
    public Role Role { get; set; }
    public long TelegramChatId { get; set; }
    public string EmailAddress { get; set; }
}
