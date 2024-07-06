using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public class UserCreateRequest
{
    [Required]
    public string LoginName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Role { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }
}
