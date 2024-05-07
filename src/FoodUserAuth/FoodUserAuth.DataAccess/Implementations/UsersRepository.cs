using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.Implementations;

public class UsersRepository : IUsersRepository, IDisposable
{
    private readonly DatabaseContext _userDbContext;
    private bool _disposedValue;

    public UsersRepository(DatabaseContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Guid</returns>

    public async Task CreateAsync(User user)
    {
        await _userDbContext.Users.AddAsync(user);
    }

    /// <summary>
    /// Update user, throw exception if user is not found.
    /// </summary>
    /// <param name="user"></param>
    public void Update(User user)
    {
        _userDbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    /// <summary>
    /// Delete user by id, if throw exception if user is not found.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(User user)
    {
        _userDbContext.Users.Remove(user);
    }

    /// <summary>
    /// Return all list of users
    /// </summary>
    /// <returns>IEnumerable<User></returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userDbContext.Users.ToListAsync();
    }

    /// <summary>
    /// Find user by name, if user is not found then return null
    /// </summary>
    /// <param name="loginName"></param>
    /// <returns>User</returns>
    public async Task<User> FindByLoginNameAsync(string loginName)
    {
        return await (from users in _userDbContext.Users
                where users.LoginName == loginName
                select users).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get user by id, throw exception if user is not found
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User</returns>
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await (from users in _userDbContext.Users
                      where users.Id == id
                      select users).FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _userDbContext.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _userDbContext.Dispose();
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
