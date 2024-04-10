using FoodUserAuth.BusinessLogic.Types;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public class VerifiedUserDto
    {
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
