using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUserVerificationService
{
    bool TryVerifyUser(string userName, string hashedPassword, out VerifiedUserDto? user);
}
