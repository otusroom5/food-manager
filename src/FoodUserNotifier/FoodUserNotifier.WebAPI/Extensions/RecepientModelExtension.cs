using FoodUserNotifier.BusinessLogic.Dto;
using FoodUserNotifier.WebApi.Models;

namespace FoodUserNotifier.WebApi.Extensions;
public static class RecepientModelExtension
{
    public static RecepientDto ToDto(this RecepientModel model)
    {
        return new RecepientDto()
        {
            EmailAddress = model.EmailAddress,
            Id = model.Id,
            Role = model.Role,
            TelegramChatId = model.TelegramChatId
        };
    }
}
