using System.ComponentModel.DataAnnotations;

namespace FoodManager.WebUI.Contracts;

public class ChangePasswordRequest
{
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string Password { get; set; }
}
