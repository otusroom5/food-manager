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

    public Guid Create(Shop shop)
    {
        var shopEntity = _mapper.Map<ShopEntity>(shop);
        var result = _repository.Create(shopEntity);
        _repository.Save();

        return result;
    }

    public Shop Get(Guid shopId)
    {
        var shopEntity = _repository.Get(shopId);
        var result = _mapper.Map<Shop>(shopEntity);

        return result;
    }

    public IEnumerable<Shop> GetAll(bool onlyActive = false)
    {
        var entities = _repository.GetAll(onlyActive);
        var result = _mapper.Map<IEnumerable<ShopEntity>, IEnumerable<Shop>>(entities);

        return result;
    }

    public void Update(Shop shop)
    {
        var shopEntity = _mapper.Map<ShopEntity>(shop);
        _repository.Update(shopEntity);
        _repository.Save();
    }

    public void Delete(Guid shopId)
    {
        _repository.Delete(shopId);
        _repository.Save();
    }
}