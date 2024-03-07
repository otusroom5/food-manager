using UserAuth.BusinessLogic.Types;

namespace UserAuth.BusinessLogic.Dto
{
    public class VerifiedUserDto
    {
        public string UserName { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
