namespace FoodUserNotifier.Infrastructure.Sources.Contracts;

internal class GenericResponse<TData> where TData : class
{
    public TData Data { get; set; }

    public string Message { get; set; }
}
