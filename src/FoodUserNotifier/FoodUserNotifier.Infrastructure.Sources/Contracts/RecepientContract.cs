namespace FoodUserNotifier.Infrastructure.Sources.Contracts;

internal sealed class RecepientContract
{
    public Guid Id { get; set; }
    public int ContactType { get; set; }
    public string Contact { get; set; }
}
