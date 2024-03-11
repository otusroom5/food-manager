using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Abstractions
{
    public interface IUsersService
    {
        Guid CreateUser(UserDto user);
        IEnumerable<UserDto> GetAll();
        void UpdateUser(UserDto user);
        void DisableUser(Guid id);
        void EnableUser(Guid id);
        void ChangePassword(Guid id);
    }
}
