using FoodUserNotifier.DataAccess.Entities;
using FoodUserNotifier.DataAccess.Interfaces;
using FoodUserNotifier.DataAccess.Types;
using Microsoft.Extensions.DependencyInjection;

namespace FoodUserNotifier.DataAccess.Implementations;

public class RecepientRepository : IRecepientRepository, IDisposable
{
    private readonly IServiceScope _serviceScope;
    private readonly DatabaseContext _context;
    private bool disposedValue;


    public RecepientRepository(IServiceScopeFactory scopeFactory)
    {
        _serviceScope = scopeFactory.CreateScope();
        _context = _serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
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

    public IEnumerable<Recepient> GetAll()
    {
        return _context.Recepients;
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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _serviceScope.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
