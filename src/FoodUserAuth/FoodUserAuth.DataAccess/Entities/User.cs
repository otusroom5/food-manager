﻿using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.DataAccess.Entities;

public sealed class User
{
    public Guid Id { get; set; }

    public string LoginName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsDisabled { get; set; }

    public string Password { get; set; }

    public UserRole Role {  get; set; }

    public ICollection<UserContact> UserContacts { get; set; }
}
