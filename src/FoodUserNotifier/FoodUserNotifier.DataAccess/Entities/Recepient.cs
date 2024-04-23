﻿using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.DataAccess.Entities;

public class Recepient
{
    public Guid Id { get; set; } 
    public Role Role { get; set; }
    public long TelegramChatId { get; set; }
    public string EmailAddress { get; set; }
}
