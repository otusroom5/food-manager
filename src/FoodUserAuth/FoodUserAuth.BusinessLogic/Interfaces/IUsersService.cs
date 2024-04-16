using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Abstractions
{
    public interface IUsersService
    {
        (UserDto User, string Password) CreateUser(UserDto user);
        IEnumerable<UserDto> GetAll();
        void UpdateUser(UserDto user);
        void DisableUser(Guid id);
        void ChangePassword(Guid id, string password);
    }
}
