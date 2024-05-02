using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models;

public class UserLoginModel
{
    [Required]
    public string LoginName { get; set; }

    [Required]
    public string Password { get; set; }
}
