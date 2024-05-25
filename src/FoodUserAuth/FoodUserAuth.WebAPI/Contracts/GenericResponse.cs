namespace FoodUserAuth.WebApi.Contracts;

internal class GenericResponse<TModel> : ResponseBase
{
    public TModel Data { get; set; }
}
