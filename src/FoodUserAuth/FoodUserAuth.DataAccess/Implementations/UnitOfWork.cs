using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Interfaces;

namespace FoodUserAuth.DataAccess.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;
    private IUsersRepository _usersRepository;
    private IApiKeyRepository _apiKeyRepository;
    private IUserContactsRepository _userContactsRepository;
    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IUsersRepository GetUsersRepository()
    {
        if (_usersRepository == null) 
        {
            _usersRepository = new UsersRepository(_databaseContext);
        }

        return _usersRepository;
    }

    public IApiKeyRepository GetApiKeyRepository()
    {
        if (_apiKeyRepository == null)
        {
            _apiKeyRepository = new ApiKeyRepository(_databaseContext);
        }

        return _apiKeyRepository;
    }


    public IUserContactsRepository GetUserContactsRepository()
    {
        if (_userContactsRepository == null)
        {
            _userContactsRepository = new UserContactsRepository(_databaseContext);
        }

        return _userContactsRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }
}
