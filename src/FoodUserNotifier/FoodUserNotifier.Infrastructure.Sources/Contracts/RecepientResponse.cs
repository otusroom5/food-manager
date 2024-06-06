namespace FoodUserNotifier.Infrastructure.Sources.Contracts;

internal class RecepientResponse
{
    public IEnumerable<RecepientContract> Data { get; set; }
    public string Message { get; set; }
}
