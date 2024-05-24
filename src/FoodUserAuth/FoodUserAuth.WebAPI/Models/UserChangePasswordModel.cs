using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models;

public class UserChangePasswordModel
{
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string Password { get; set; }
}