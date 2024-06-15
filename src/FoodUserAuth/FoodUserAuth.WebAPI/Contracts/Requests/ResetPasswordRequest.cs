using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public class ResetPasswordRequest
{
    [Required]
    public string UserId { get; set; }
}
