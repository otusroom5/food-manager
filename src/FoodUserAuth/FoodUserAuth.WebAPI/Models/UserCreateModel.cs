using System;

namespace FoodUserAuth.WebApi.Models;

public class UserCreateModel
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
}
