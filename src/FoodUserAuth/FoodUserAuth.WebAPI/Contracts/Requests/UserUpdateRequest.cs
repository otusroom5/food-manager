using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public class UserUpdateRequest
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
