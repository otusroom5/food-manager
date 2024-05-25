using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.Implementations;

public class UsersRepository : IUsersRepository, IDisposable
{
    private readonly DatabaseContext _dbContext;
    private bool _disposedValue;

    public UsersRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Guid</returns>

    public void Create(User user)
    {
        _dbContext.Users.Add(user);
    }

    /// <summary>
    /// Update user, throw exception if user is not found.
    /// </summary>
    /// <param name="user"></param>
    public void Update(User user)
    {
        _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    /// <summary>
    /// Delete user by id, if throw exception if user is not found.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    /// <summary>
    /// Return all list of users
    /// </summary>
    /// <returns>IEnumerable<User></returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.OrderBy(f => f.Id).ToListAsync();
    }

    /// <summary>
    /// Find user by name, if user is not found then return null
    /// </summary>
    /// <param name="loginName"></param>
    /// <returns>User</returns>
    public async Task<User> FindByLoginNameAsync(string loginName)
    {
        return await (from users in _dbContext.Users
                      where EF.Functions.ILike(users.LoginName,$"%{loginName}%")
                      select users).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get user by id, throw exception if user is not found
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User</returns>
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await (from users in _dbContext.Users
                      where users.Id == id
                      select users).FirstOrDefaultAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _dbContext.Dispose();
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
