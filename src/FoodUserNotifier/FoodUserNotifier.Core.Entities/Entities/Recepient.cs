using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Core.Entities;

public sealed class Recepient
{
    public Guid Id { get; set; }
    public ContactType ContactType { get; set; }
    public string Address { get; set; }
}
