using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.BusinessLogic.Extensions;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.WebApi.Utils;

namespace FoodUserAuth.BusinessLogic.Implementations;

public class ApiKeyService : IApiKeyService
{
    private readonly ITokenHandler _tokenHandler;
    private readonly IApiKeyRepository _apiKeyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public ApiKeyService(IUnitOfWork unitOfWork, 
        ITokenHandler tokenHandler,
        ICurrentUserAccessor currentUserAccessor)
    {
        _unitOfWork = unitOfWork;
        _apiKeyRepository = unitOfWork.GetApiKeyRepository();
        _tokenHandler = tokenHandler;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<ApiKeyDto> CreateApiKeyAsync(DateTime expiryDate)
    {
        UserDto currentUser = await _currentUserAccessor.GetCurrentUserAsync();

        if (currentUser == null)
        {
            throw new AccessViolationException("Api Keys can not be created by predefined user");
        }

        Guid newId = Guid.NewGuid();
        var apiKey = new ApiKeyDto()
        {
            Id = newId,
            ExpiryDate = expiryDate,
            Key = _tokenHandler.GenerateApiToken(newId, currentUser.Id)
        };

        _apiKeyRepository.Create(apiKey.ToModel());

        await _unitOfWork.SaveChangesAsync();

        return apiKey;
    }

    public async Task<IEnumerable<ApiKeyDto>> GetKeysAsync()
    {
        IEnumerable<ApiKey> keys = await _apiKeyRepository.GetAllAsync();
        return keys.Select(f => f.ToDto());
    }

    public async Task<string> RenewApiKeyAsync(string token)
    {
        ApiKeyData apiTokenData = _tokenHandler.ExtractApiKeyData(token);
        
        if (IsValidToken(apiTokenData.ValidTo)) 
        {
            return token;
        }

        ApiKey apiKey = await _apiKeyRepository.GetByIdOrDefaultAsync(apiTokenData.KeyId);

        if (apiKey == default)
        {
            throw new InvalidApiKeyException();
        }

        if (!IsValidToken(apiKey.ExpiryDate))
        {
            throw new InvalidApiKeyException();
        }

        return _tokenHandler.GenerateApiToken(apiTokenData.KeyId, apiTokenData.UserId);
    }

    private bool IsValidToken(DateTime validTo) 
    { 
        return validTo >= DateTime.Now;
    }

    public async Task DeleteAsync(Guid id)
    {
        ApiKey key = await _apiKeyRepository.GetByIdOrDefaultAsync(id);
        
        if (key == default) 
        { 
            throw new UserNotFoundException();
        }

        _apiKeyRepository.Delete(key);

        await _unitOfWork.SaveChangesAsync();
    }

}
