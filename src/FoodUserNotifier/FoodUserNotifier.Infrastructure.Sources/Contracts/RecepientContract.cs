using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Infrastructure.Sources.Contracts;

public sealed class RecepientContract
{
    public Guid Id { get; set; }
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
}
