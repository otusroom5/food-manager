using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Extensions;

namespace FoodUserAuth.BusinessLogic.Services;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    
    public CurrentUserAccessor(IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserDto> GetCurrentUserAsync()
    {
        Guid currentId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty);

        if (currentId == Guid.Empty)
        {
            throw new InvalidUserIdException();
        }

        User user = await _unitOfWork.GetUsersRepository().GetByIdAsync(currentId);

        return user?.ToDto();
    }
}
