using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Infrastructure.Sources.Contracts;

public sealed class RecepientModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; }  
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
}
