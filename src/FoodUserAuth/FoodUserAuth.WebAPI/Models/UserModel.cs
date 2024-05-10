﻿using FoodUserAuth.DataAccess.Types;
using System.Text.Json.Serialization;

namespace FoodUserAuth.WebApi.Models;
public class UserModel
{
    public string Id { get; set; }
    
    public string LoginName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public UserRole Role { get; set; }
    
    public string Email { get; set; }
    
    public bool IsDisabled { get; set; }
}
