using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses
{
    public class UsersResponse : ResponseBase
    {
        public User[] Data { get; set; }
    }
}
