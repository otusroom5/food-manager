﻿namespace FoodUserAuth.WebApi.Models;

public class UserUpdateModel
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
}