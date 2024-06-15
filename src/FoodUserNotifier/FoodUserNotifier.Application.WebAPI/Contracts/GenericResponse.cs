namespace FoodUserNotifier.Application.WebAPI.Contracts;

internal class GenericResponse<TModel> : ResponseBase
{
    public TModel Data { get; set; }
}
