using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.DataAccess.Interfaces;

public interface IUserContactsRepository
{
    void Create(UserContact user);
    void Update(UserContact user);
    void Delete(UserContact user);
    Task<UserContact> GetByUserIdAndContactTypeAsync(Guid userId, UserContactType contactType, bool include);
    Task<IEnumerable<UserContact>> GetAllAsync();
    Task<IEnumerable<UserContact>> GetAllForRoleAsync(UserRole role, bool include);
    Task<UserContact> Find(UserContactType contactType, string contact, bool include);
}
