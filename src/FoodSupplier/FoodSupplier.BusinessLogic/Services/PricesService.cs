using AutoMapper;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.DataAccess.Abstractions;
using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.BusinessLogic.Services;

public class PricesService : IPricesService
{
    private readonly IPricesRepository _repository;
    private readonly IMapper _mapper;

    public PricesService(IPricesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(PriceEntry priceEntry)
    {
        var priceEntity = _mapper.Map<PriceEntryEntity>(priceEntry);
        var result = await _repository.CreateAsync(priceEntity);
        await _repository.SaveAsync();

        return result;
    }

    public async Task<PriceEntry> GetAsync(Guid priceEntryId)
    {
        var priceEntity = await _repository.GetAsync(priceEntryId);
        var result = _mapper.Map<PriceEntry>(priceEntity);

        return result;
    }

    public async Task<PriceEntry> GetLastAsync(Guid productId)
    {
        var priceEntity = await _repository.GetLastAsync(productId);
        var result = _mapper.Map<PriceEntry>(priceEntity);

        return result;
    }

    public async Task<IEnumerable<PriceEntry>> GetAllAsync(Guid productId)
    {
        var priceEntities = await _repository.GetAllAsync(productId);
        var result = _mapper.Map<IEnumerable<PriceEntryEntity>, IEnumerable<PriceEntry>>(priceEntities);

        return result;
    }
}