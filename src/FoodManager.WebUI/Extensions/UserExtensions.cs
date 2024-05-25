using FoodManager.WebUI.Areas.Administrator.Contracts;
using FoodManager.WebUI.Areas.Administrator.Models;

namespace FoodManager.WebUI.Extensions;

public static class UserExtensions
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel()
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsDisabled = user.IsDisabled,
            LoginName = user.LoginName,
            Role = user.Role
        };
    }
}
