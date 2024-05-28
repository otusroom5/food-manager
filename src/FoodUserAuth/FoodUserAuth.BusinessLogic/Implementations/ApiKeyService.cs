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
    private readonly ITokenHandler _tokenGenerator;
    private readonly IApiKeyRepository _apiKeyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApiKeyService(IUnitOfWork unitOfWork, ITokenHandler tokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _apiKeyRepository = unitOfWork.GetApiKeyRepository();
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ApiKeyDto> CreateApiKeyAsync(DateTime expiryDate)
    {
        Guid newId = Guid.NewGuid();
        var apiKey = new ApiKeyDto()
        {
            Id = newId,
            ExpiryDate = expiryDate,
            Token = _tokenGenerator.GenerateApiToken(newId)
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
        (Guid apiKeyId, DateTime validTo) = _tokenGenerator.ExtractApiKeyData(token);
        
        if (IsValidToken(validTo)) 
        {
            return token;
        }

        ApiKey apiKey = await _apiKeyRepository.GetByIdOrDefaultAsync(apiKeyId);

        if (apiKey == default)
        {
            throw new InvalidApiKeyException();
        }

        (_, DateTime dbTokenValidTo) = _tokenGenerator.ExtractApiKeyData(apiKey.Token);

        if (IsValidToken(dbTokenValidTo))
        {
            return apiKey.Token;
        }

        apiKey.Token = _tokenGenerator.GenerateApiToken(apiKeyId);

        _apiKeyRepository.Update(apiKey);

        await _unitOfWork.SaveChangesAsync();

        return apiKey.Token;
    }

    private bool IsValidToken(DateTime validTo) 
    { 
        return validTo <= DateTime.Now;
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
