using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface ICurrentUserAccessor
{
    Task<User> GetCurrentUserAsync();
}