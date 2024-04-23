using FoodUserNotifier.BusinessLogic.Dto;
using FoodUserNotifier.DataAccess.Entities;

namespace FoodUserNotifier.BusinessLogic.Extensions;

public static class RecepientExtension
{
    public static RecepientDto ToDto(this Recepient recepient)
    {
        return new RecepientDto()
        {
            Id = recepient.Id,
            EmailAddress = recepient.EmailAddress,
            Role = recepient.Role,
            TelegramChatId = recepient.TelegramChatId
        };
    }
}
