using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(UserDto userDto);
    }
}
