using AutoMapper;
using FoodSupplier.BusinessLogic.Abstractions;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.DataAccess.Abstractions;
using FoodSupplier.DataAccess.Entities;

namespace FoodSupplier.BusinessLogic.Services;

public class ShopsService : IShopsService
{
    private readonly IShopsRepository _repository;
    private readonly IMapper _mapper;

    public ShopsService(IShopsRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(Shop shop)
    {
        var shopEntity = _mapper.Map<ShopEntity>(shop);
        var result = await _repository.CreateAsync(shopEntity);
        await _repository.SaveAsync();

        return result;
    }

    public async Task<Shop> GetAsync(Guid shopId)
    {
        var shopEntity = await _repository.GetAsync(shopId);
        var result = _mapper.Map<Shop>(shopEntity);

        return result;
    }

    public async Task<IEnumerable<Shop>> GetAllAsync(bool onlyActive = false)
    {
        var entities = await _repository.GetAllAsync(onlyActive);
        var result = _mapper.Map<IEnumerable<ShopEntity>, IEnumerable<Shop>>(entities);

        return result;
    }

    public void Update(Shop shop)
    {
        var shopEntity = _mapper.Map<ShopEntity>(shop);
        _repository.Update(shopEntity);
        _repository.SaveAsync();
    }

    public void Delete(Guid shopId)
    {
        _repository.DeleteAsync(shopId);
        _repository.SaveAsync();
    }
}