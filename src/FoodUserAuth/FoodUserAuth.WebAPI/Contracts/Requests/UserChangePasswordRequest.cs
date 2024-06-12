using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public class UserChangePasswordRequest
{
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string Password { get; set; }
}