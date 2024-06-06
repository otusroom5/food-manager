using FoodUserAuth.DataAccess.Types;
using System;

namespace FoodUserAuth.WebApi.Models;

public sealed class UserContactModel
{
    public Guid Id { get; set; }
    public UserContactType ContactType { get; set; }
    public string Contact { get; set; }
}
