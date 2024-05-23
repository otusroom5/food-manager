using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;
using System;

namespace FoodUserAuth.WebApi.Extensions;

public static class UserModelExtensions
{
    public static UserDto ToDto(this UserModel model)
    {
        return new UserDto()
        {
            Id = Guid.Parse(model.Id),
            LoginName = model.LoginName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            IsDisabled = model.IsDisabled,
            Role = model.Role
        };
    }
}
