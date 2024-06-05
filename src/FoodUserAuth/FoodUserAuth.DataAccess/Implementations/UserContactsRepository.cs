using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Types;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.Implementations;

internal class UserContactsRepository : IUserContactsRepository
{
    private readonly DatabaseContext _dataContext;

    public UserContactsRepository(DatabaseContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void Create(UserContact user)
    {
        _dataContext.UserContacts.Add(user);
    }

    public void Delete(UserContact user)
    {
        _dataContext.UserContacts.Remove(user);
    }

    public async Task<IEnumerable<UserContact>> GetAllAsync()
    {
        return await _dataContext.UserContacts.OrderBy(f => f.Id).ToListAsync();
    }

    public async Task<IEnumerable<UserContact>> GetAllForRoleAsync(UserRole role)
    {
        return await (from users in _dataContext.Users
                      join contacts in _dataContext.UserContacts on users.Id equals contacts.UserId
                      where role == users.Role
                      select contacts).ToListAsync();
     }

    public async Task<UserContact> GetByUserIdAndContactTypeAsync(Guid userId, UserContactType contactType)
    {
        return await (from users in _dataContext.Users
                     join contacts in _dataContext.UserContacts on users.Id equals contacts.UserId
                     where users.Id == userId && contacts.ContactType == contactType
                     select contacts).FirstOrDefaultAsync();
    }

    public void Update(UserContact user)
    {
        _dataContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
}
