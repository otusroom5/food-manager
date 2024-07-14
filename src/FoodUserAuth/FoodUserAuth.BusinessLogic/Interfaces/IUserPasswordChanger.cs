using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUserPasswordChanger
{
    Task ChangeAsync(User user, string oldPassword, string newPassword);
    Task<string> ResetAsync(Guid userId);
}