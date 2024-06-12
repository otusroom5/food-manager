using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.WebApi.Contracts.Requests;

public sealed class GetContactRequest
{
    public UserContactType ContactType { get; set; }
    public string Contact { get; set; }
}
