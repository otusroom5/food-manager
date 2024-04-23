using FoodUserNotifier.BusinessLogic.Dto;
using FoodUserNotifier.WebApi.Models;

namespace FoodUserNotifier.WebApi.Extensions;

public static class RecepientDtoExtension
{
    public static RecepientModel ToModel(this RecepientDto recepient)
    {
        return new RecepientModel()
        {
            Id = recepient.Id,
            EmailAddress = recepient.EmailAddress,
            Role = recepient.Role,
            TelegramChatId = recepient.TelegramChatId
        };
    }
}
