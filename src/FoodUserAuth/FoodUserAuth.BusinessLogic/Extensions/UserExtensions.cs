using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Entities;


namespace FoodUserAuth.BusinessLogic.Extensions;

public static class UserExtensions
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
