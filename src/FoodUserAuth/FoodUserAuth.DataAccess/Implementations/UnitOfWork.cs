using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Interfaces;

namespace FoodUserAuth.DataAccess.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;
    private IUsersRepository _usersRepository;
    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IUsersRepository GetUsersRepository()
    {
        if (_usersRepository == null ) 
        {
            _usersRepository = new UsersRepository(_databaseContext);
        }

        return _usersRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }
}
