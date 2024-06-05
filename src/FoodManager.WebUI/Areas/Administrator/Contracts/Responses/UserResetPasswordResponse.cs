using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses;

public sealed class UserResetPasswordResponse : ResponseBase
{
    public UserResetPasswordData Data { get; set; }
}

public sealed class UserResetPasswordData
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
}