using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.DataAccess.Entities;

public record Recepient
{
    public Role RoleEnum { get; set; }
    public string Address { get; set; }
}
