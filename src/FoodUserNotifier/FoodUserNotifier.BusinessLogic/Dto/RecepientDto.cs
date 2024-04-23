using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.BusinessLogic.Dto;

public class RecepientDto
{
    public Guid Id { get; set; }
    public Role Role { get; set; }
    public long TelegramChatId { get; set; }
    public string EmailAddress { get; set; }
}
