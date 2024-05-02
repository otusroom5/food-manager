using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;
using System.Data;

namespace FoodUserAuth.BusinessLogic.Extensions;

public static class UserDtoExtension
{
    public static User ToEntity(this UserDto user)
    {
        return new User()
        {
            Id = user.Id,
            LoginName = user.LoginName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsDisabled = user.IsDisabled
        };
    }
}
