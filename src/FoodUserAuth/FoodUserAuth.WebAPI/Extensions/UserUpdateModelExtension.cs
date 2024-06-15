using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Contracts.Requests;
using System;

namespace FoodUserAuth.WebApi.Extensions;

internal static class UserUpdateModelExtension
{
    public static UserDto ToDto(this UserUpdateRequest model)
    {
        return new UserDto()
        {
            Id = Guid.Parse(model.UserId),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = Enum.Parse<DataAccess.Types.UserRole>(model.Role)
        };
    }
}
