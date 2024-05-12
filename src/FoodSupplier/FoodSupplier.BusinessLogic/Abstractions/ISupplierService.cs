namespace FoodSupplier.BusinessLogic.Abstractions;

public interface ISupplierService
{
    void Produce();
    void Produce(Guid shopId, Guid productId);
}