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
    public string Email { get; set; }
    public string Telegram { get; set; }
}
