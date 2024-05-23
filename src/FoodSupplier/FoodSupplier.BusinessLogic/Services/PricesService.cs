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

    public Guid Create(PriceEntry priceEntry)
    {
        var priceEntity = _mapper.Map<PriceEntryEntity>(priceEntry);
        var result = _repository.Create(priceEntity);
        _repository.Save();

        return result;
    }

    public PriceEntry Get(Guid priceEntryId)
    {
        var priceEntity = _repository.Get(priceEntryId);
        var result = _mapper.Map<PriceEntry>(priceEntity);

        return result;
    }

    public PriceEntry GetLast(Guid productId)
    {
        var priceEntity = _repository.GetLast(productId);
        var result = _mapper.Map<PriceEntry>(priceEntity);

        return result;
    }
}