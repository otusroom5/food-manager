using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Types;
using FoodUserAuth.WebApi.Models;
using System;

namespace FoodUserAuth.WebApi.Extensions;

public static class UserCreateModelExtension
{
    public static UserDto ToDto(this UserCreateModel model) 
    {
        return new UserDto()
        {
            LoginName = model.LoginName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = Enum.Parse<UserRole>(model.Role)
        };
    }
}
