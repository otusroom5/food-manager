using FoodUserNotifier.BusinessLogic.Dto;
using FoodUserNotifier.BusinessLogic.Extensions;
using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Interfaces;

namespace FoodUserNotifier.BusinessLogic.Implementations;

public class RecepientService : IRecepientService
{
    private readonly IRecepientRepository _repository;
    public RecepientService(IRecepientRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<RecepientDto> GetAll()
    {
        return _repository.GetAll().Select(item => item.ToDto());
    }

    public void Create(RecepientDto recepientDto)
    {
        _repository.Create(recepientDto.ToEntity());
    }

    public void Update(RecepientDto recepientDto)
    {
        _repository.Update(recepientDto.ToEntity());
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }
}
