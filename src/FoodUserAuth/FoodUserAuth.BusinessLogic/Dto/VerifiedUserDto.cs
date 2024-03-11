using FoodUserAuth.BusinessLogic.Types;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public class VerifiedUserDto
    {
        public string UserName { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
