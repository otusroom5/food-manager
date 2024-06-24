using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;
using System;

namespace FoodUserAuth.WebApi.Extensions;

internal static class UserModelExtensions
{
    public static UserDto ToDto(this UserModel model)
    {
        return new UserDto()
        {
            Id = Guid.Parse(model.UserId),
            LoginName = model.LoginName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Telegram = model.Telegram,
            IsDisabled = model.IsDisabled,
            Role = model.Role
        };
    }
}
