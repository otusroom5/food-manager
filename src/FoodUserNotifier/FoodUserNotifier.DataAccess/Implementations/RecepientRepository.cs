using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.DataAccess.Interfaces;
using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.DataAccess.Implementations;

public class RecepientRepository : IRecepientRepository
{
    private readonly DatabaseContext _context;
    
    public RecepientRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Recepient Create(Recepient recepient)
    {
        _context.Add(recepient);
        return recepient;
    }

    public void Delete(Guid id)
    {
        Recepient foundRecepient = GetById(id);
        _context.Recepients.Remove(foundRecepient);
        _context.SaveChanges();
    }

    public IEnumerable<Recepient> GetAllForRole(Role role)
    {
        return _context.Recepients.Where(item => item.Role == role);
    }

    public Recepient GetById(Guid id)
    {
        return _context.Recepients.FirstOrDefault(item => item.Id.Equals(id));
    }

    public void Update(Recepient recepient)
    {
        _context.Entry(recepient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
}
