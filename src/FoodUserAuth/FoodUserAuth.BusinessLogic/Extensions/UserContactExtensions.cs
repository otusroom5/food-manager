using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Extensions;

public static class UserContactExtensions
{
    public static UserContactDto ToDto(this UserContact contact)
    {
        return new UserContactDto()
        {
            Id = contact.Id,
            Contact = contact.Contact,
            ContactType = contact.ContactType,
            UserId = contact.UserId
        };
    }

}
