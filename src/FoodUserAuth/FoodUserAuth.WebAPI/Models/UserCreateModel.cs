using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Models;

public class UserCreateModel
{
    [Required]
    public string LoginName {  get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string Email { get; set; }
}
