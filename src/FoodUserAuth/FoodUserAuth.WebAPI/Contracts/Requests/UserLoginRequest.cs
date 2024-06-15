using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public class UserLoginRequest
{
    [Required]
    public string LoginName { get; set; }

    [Required]
    public string Password { get; set; }

}
