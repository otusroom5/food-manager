using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.BusinessLogic.Dto;

public class UserContactDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserContactType ContactType { get; set; }
    public string Contact { get; set; }
}
