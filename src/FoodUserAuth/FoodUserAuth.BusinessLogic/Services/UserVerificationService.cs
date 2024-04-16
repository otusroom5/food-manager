using System.Security.Cryptography;
using FoodUserAuth.BusinessLogic.Abstractions;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Services
{
    public partial class UserVerificationService : IUserVerificationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserVerificationService(IUsersRepository usersRepository, IPasswordHasher passwordHasher) 
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// This method verify user by username and 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="user"></param>
        /// <returns>True if user is valid otherwise false, also return out parameter with detailed user</returns>
        /// <remarks>
        public bool TryVerifyUser(string userName, string hashedPassword, out VerifiedUserDto? user)
        {
            user = null;
            User? foundUser = _usersRepository.FindUserByLoginName(userName);

            if (foundUser is null)
            {
                return false;
            }

            return _passwordHasher.VerifyHash(foundUser.Password, hashedPassword);
        }
    }
}
