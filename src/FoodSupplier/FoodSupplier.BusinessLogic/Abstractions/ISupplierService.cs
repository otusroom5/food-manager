namespace FoodSupplier.BusinessLogic.Abstractions;

public interface ISupplierService
{
    Task ProduceAsync();
    Task ProduceAsync(Guid shopId, Guid productId);
}