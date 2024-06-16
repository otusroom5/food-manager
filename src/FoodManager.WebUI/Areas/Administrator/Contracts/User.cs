﻿namespace FoodManager.WebUI.Areas.Administrator.Contracts;

public class User
{
    public string UserId { get; set; }
    public string LoginName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }
    public bool IsDisabled { get; set; }
}
