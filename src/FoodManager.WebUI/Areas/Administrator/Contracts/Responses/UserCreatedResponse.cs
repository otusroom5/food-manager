using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses;

public sealed class UserCreatedResponse : ResponseBase
{
    public UserCreatedData Data { get; set; }
}

public sealed class UserCreatedData
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
}