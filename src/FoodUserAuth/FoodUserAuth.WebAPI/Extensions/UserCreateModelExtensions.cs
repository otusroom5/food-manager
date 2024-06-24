using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Types;
using FoodUserAuth.WebApi.Contracts.Requests;
using System;

namespace FoodUserAuth.WebApi.Extensions;

internal static class UserCreateModelExtensions
{
    public static UserDto ToDto(this UserCreateRequest model) 
    {
        return new UserDto()
        {
            LoginName = model.LoginName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Telegram = model.Telegram,
            Role = Enum.Parse<UserRole>(model.Role)
        };
    }
}
