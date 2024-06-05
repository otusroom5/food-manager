using System;

namespace FoodUserAuth.WebApi.Models;

public sealed class ApiKeyModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public DateTime ExpiryDate { get; set; }
}