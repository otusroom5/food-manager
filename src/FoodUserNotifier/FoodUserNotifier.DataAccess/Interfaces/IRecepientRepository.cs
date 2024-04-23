using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.DataAccess.Interfaces;

public interface IRecepientRepository
{
    Recepient Create(Recepient recepient);
    void Update(Recepient recepient);
    void Delete(Guid id);
    Recepient GetById(Guid id);
    IEnumerable<Recepient> GetAllForRole(Role role);
    IEnumerable<Recepient> GetAll();
}
