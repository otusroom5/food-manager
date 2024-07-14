using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;

namespace FoodUserAuth.BusinessLogic.Services;

public class UserPasswordChanger : IUserPasswordChanger
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordGenerator _passwordGenerator;

    public UserPasswordChanger(IUnitOfWork unitOfWork, 
        IPasswordHasher passwordHasher,
        IPasswordGenerator passwordGenerator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
    }

    public async Task ChangeAsync(User user, string oldPassword, string newPassword)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userForUpdate = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(user.LoginName);

        if (userForUpdate == null)
        {
            throw new UserNotFoundException();
        }

        userForUpdate.Password = _passwordHasher.ComputeHash(newPassword);

        _unitOfWork.GetUsersRepository().Update(userForUpdate);

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<string> ResetAsync(Guid userId)
    {
        User foundUser = await _unitOfWork.GetUsersRepository().GetByIdAsync(userId);

        string newPassword = _passwordGenerator.GeneratePassword(foundUser.LoginName);
        foundUser.Password = _passwordHasher.ComputeHash(newPassword);

        _unitOfWork.GetUsersRepository().Update(foundUser);

        await _unitOfWork.SaveChangesAsync();

        return newPassword;
    }

}
