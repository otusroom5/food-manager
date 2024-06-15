using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.DataAccess.Entities;

public sealed class UserContact
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public UserContactType ContactType { get; set; }
    public string Contact {  get; set; }
    public User User { get; set; }
}
