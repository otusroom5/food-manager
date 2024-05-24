using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Administrator.Models;

public class UserIndexModel
{
    public UserModel[] Users { get; set; }

    public UserModel User { get; set; }
}

public class UserModel
{
    [HttpTableColumnKey]
    public string UserId { get; set; }

    [HttpTableColumn("Login")]
    public string LoginName { get; set; }

    [HttpTableColumn("First Name")]
    public string FirstName { get; set; }

    [HttpTableColumn("Last Name")]
    public string LastName { get; set; }

    [HttpTableColumn("Role")]
    public string Role { get; set; }

    [HttpTableColumn("Email")]
    public string Email { get; set; }

    [HttpTableColumn("Disabled")]
    public bool IsDisabled { get; set; }
}