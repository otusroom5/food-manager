using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models;

public class UserUpdateModel
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string Email { get; set; }
}
