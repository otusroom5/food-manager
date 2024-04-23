using FoodUserNotifier.BusinessLogic.Dto;
using FoodUserNotifier.DataAccess.Entities;

namespace FoodUserNotifier.BusinessLogic.Extensions;

public static class RecepientDtoExtension
{
    public static Recepient ToEntity(this RecepientDto recepient)
    {
        return new Recepient()
        {
            Id = recepient.Id,
            EmailAddress = recepient.EmailAddress,
            Role = recepient.Role,
            TelegramChatId = recepient.TelegramChatId
        };
    }
}
