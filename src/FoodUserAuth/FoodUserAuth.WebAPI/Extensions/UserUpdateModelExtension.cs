using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;
using System;

namespace FoodUserAuth.WebApi.Extensions
{
    public static class UserUpdateModelExtension
    {
        public static UserDto ToDto(this UserUpdateModel model)
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
}
