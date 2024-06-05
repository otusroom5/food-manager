namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IPdfHandlerService
{
    public byte[] CreateDocument(string text);
}
