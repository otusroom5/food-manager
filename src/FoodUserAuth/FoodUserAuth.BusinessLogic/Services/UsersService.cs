using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Abstractions;
using FoodUserAuth.DataAccess.Abstractions;
using static FoodUserAuth.BusinessLogic.Services.UserVerificationService;

namespace FoodUserAuth.BusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher) 
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// This method change user password
        /// </summary>
        public void ChangePassword(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method create user
        /// </summary>
        public Guid CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method disable exist user
        /// </summary>
        public void DisableUser(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method enable exist user
        /// </summary>
        public void EnableUser(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method return all available users
        /// </summary>
        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method update detaisl of user
        /// </summary>
        public void UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
