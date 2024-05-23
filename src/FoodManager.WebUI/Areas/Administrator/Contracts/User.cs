namespace FoodManager.WebUI.Areas.Administrator.Contracts;

public class User
{
    public string Id { get; set; }
    public string LoginName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public bool IsDisabled { get; set; }
}
