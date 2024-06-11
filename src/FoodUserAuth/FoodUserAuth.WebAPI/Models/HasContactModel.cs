using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.WebApi.Models;

public sealed class HasContactModel
{
    public UserContactType ContactType { get; set; }
    public string Contact { get; set; }
}
