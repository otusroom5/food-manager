using System.ComponentModel.DataAnnotations;

namespace UserAuth.WebApi.Models
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string HashedPassword { get; set; } = null!;
    }
}
