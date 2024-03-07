using UserAuth.BusinessLogic.Dto;

namespace UserAuth.BusinessLogic.Abstractions
{
    public interface IUserVerificationService
    {
        bool TryVerifyUser(string userName, string hashedPassword, out VerifiedUserDto? user);
    }
}
