using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses;

public sealed class UsersResponse : ResponseBase
{
    public User[] Data { get; set; }
}
