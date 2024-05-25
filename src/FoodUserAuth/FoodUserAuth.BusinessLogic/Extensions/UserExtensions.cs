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
            Email = user.Email,
            IsDisabled = user.IsDisabled
        };
    }
}
