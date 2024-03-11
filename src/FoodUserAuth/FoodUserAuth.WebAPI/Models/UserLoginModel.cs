using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string HashedPassword { get; set; } = null!;
    }
}
