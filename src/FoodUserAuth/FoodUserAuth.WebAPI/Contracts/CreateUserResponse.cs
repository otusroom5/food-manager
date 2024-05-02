using System;

namespace FoodUserAuth.WebApi.Contracts
{
    public class CreateUserResponse
    {
        public Guid UserId {  get; set; }   
        public string Password { get; set; }
    }
}
