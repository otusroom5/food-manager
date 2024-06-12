using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Infrastructure.Sources.Contracts.Requests;

internal class FindRecepientRequest
{
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
}
