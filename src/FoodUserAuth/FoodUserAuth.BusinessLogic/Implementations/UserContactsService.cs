using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Extensions;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Types;
using System.Data;

namespace FoodUserAuth.DataAccess.Implementations;

public class UserContactsService : IUserContactsService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserContactsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserContactDto>> GetAllForRoleAsync(UserRole role)
    {
        var userContacts = await _unitOfWork
            .GetUserContactsRepository()
            .GetAllForRoleAsync(role, false);

        return userContacts.Select(f => f.ToDto());
    }

    public async Task<bool> HasContact(UserContactType сontactType, string contact)
    {
        var userContact = await _unitOfWork
            .GetUserContactsRepository()
            .FindContact(сontactType, contact, false);

        return userContact != null;
    }
}
