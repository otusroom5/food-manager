using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;

namespace FoodUserAuth.WebApi.Extensions;

internal static class UserDtoExtensions
{
    public static UserModel ToModel(this UserDto dto)
    {
        return new UserModel()
        {
            UserId = dto.Id.ToString(),
            LoginName = dto.LoginName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telegram = dto.Telegram,
            IsDisabled = dto.IsDisabled,
            Role = dto.Role
        };
    }
}
