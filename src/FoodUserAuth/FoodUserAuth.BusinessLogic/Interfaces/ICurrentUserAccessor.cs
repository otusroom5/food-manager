using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface ICurrentUserAccessor
{
    Task<UserDto> GetCurrentUserAsync();
}