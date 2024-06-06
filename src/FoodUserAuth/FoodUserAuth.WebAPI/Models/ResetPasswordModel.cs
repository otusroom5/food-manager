using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models;

public class ResetPasswordModel
{
    [Required]
    public string UserId { get; set; }
}
