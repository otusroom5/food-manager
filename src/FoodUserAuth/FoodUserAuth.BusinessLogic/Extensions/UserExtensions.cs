using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Extensions;

internal static class UserExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            LoginName = user.LoginName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsDisabled = user.IsDisabled,
            Email = user.UserContacts?.FirstOrDefault(f => f.ContactType == DataAccess.Types.UserContactType.Email)?.Contact ?? string.Empty,
            Telegram = user.UserContacts?.FirstOrDefault(f => f.ContactType == DataAccess.Types.UserContactType.Telegram)?.Contact ?? string.Empty
        };
    }
}
