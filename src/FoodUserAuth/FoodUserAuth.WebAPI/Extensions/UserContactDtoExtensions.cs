using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;

namespace FoodUserAuth.WebApi.Extensions;

public static class UserContactDtoExtensions
{
    public static UserContactModel ToModel(this UserContactDto contactDto)
    {
        return new UserContactModel()
        {
            Contact = contactDto.Contact,
            ContactType = contactDto.ContactType,
            UserId = contactDto.UserId
        };
    }
}
