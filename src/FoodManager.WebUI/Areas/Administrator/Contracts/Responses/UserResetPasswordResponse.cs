using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses;

public class UserResetPasswordResponse : ResponseBase
{
    public UserResetPasswordData Data { get; set; }
}

public class UserResetPasswordData
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
}