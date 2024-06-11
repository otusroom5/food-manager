using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Types;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<UserContact> FindContact(UserContactType contactType, string contact, bool include)
    {
        string contactText = contact.ToLower();

        var query = from contacts in _dataContext.UserContacts
                    where contacts.Contact.ToLower() == contactText &&
                    contacts.ContactType == contactType
                    select contacts;

        if (include)
            query = query.Include(c => c.User);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserContact>> GetAllAsync()
    {
        return await _dataContext
            .UserContacts
            .Include(c => c.User)
            .OrderBy(f => f.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserContact>> GetAllForRoleAsync(UserRole role, bool include)
    {
        var query = from users in _dataContext.Users
                    join contacts in _dataContext.UserContacts on users.Id equals contacts.UserId
                    where role == users.Role
                    select contacts;

        if (include)
            query = query.Include(c => c.User);

        return await query.ToListAsync();
    }

    public async Task<UserContact> GetByUserIdAndContactTypeAsync(Guid userId, UserContactType contactType, bool include)
    {
        var query = from users in _dataContext.Users
                    join contacts in _dataContext.UserContacts on users.Id equals contacts.UserId
                    where users.Id == userId && contacts.ContactType == contactType
                    select contacts;
        
        if (include)
            query = query.Include(c => c.User);

        return await query.FirstOrDefaultAsync();
    }

    public void Update(UserContact user)
    {
        _dataContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
}
