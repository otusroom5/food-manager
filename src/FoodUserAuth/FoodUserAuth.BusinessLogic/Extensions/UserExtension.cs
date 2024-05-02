using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;
using System.Data;

namespace FoodUserAuth.BusinessLogic.Extensions;

public static class UserExtension
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
