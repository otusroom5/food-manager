using FoodUserNotifier.BusinessLogic.Dto;

namespace FoodUserNotifier.BusinessLogic.Interfaces;

public interface IRecepientService
{
    void Create(RecepientDto recepientDto);
    void Delete(Guid id);
    IEnumerable<RecepientDto> GetAll();
    void Update(RecepientDto recepientDto);
}