namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReport
{
    Task <byte[]> PrepareAsync();
}
