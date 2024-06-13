namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IPdfService
{ 
    public Task<byte[]> CreatePDFAsync(string htmlContent);
}