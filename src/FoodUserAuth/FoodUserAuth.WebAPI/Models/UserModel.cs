using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.WebApi.Models;

public class UserModel
{
    public string UserId { get; set; }
    
    public string LoginName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public UserRole Role { get; set; }
    
    public string Email { get; set; }
    
    public string Telegram { get; set; }

    public bool IsDisabled { get; set; }
}
