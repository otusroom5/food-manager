using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Extensions;

internal static class UserDtoExtensions
{
    public static User ToEntity(this UserDto user)
    {
        return new User()
        {
            Id = user.Id,
            LoginName = user.LoginName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsDisabled = user.IsDisabled
        };
    }
}
