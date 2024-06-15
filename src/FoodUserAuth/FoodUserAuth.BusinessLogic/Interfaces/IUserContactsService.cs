﻿using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUserContactsService
{
    Task<IEnumerable<UserContactDto>> GetAllForRoleAsync(UserRole role);
    Task<UserContactDto> FindContact(UserContactType сontactType, string contact);
}
