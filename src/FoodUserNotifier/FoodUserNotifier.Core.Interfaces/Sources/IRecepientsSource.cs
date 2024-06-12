using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Core.Interfaces.Sources;

public interface IRecepientsSource
{
    Task<IEnumerable<Recepient>> GetAllByRecepientGroupAsync(RecepientGroupType recepientGroupType);
    Task<Recepient> FindAsync(ContactType contactType, string contact);
}
